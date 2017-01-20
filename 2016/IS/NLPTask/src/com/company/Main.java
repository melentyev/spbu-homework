package com.company;

// Extracting keywords, collocations
// 1. normalize text
//   a. break lines by delimiters
//   b. lowercase, ё -> e
//   c. remove stopwords
// 2. count ngrams frequencies
//   a. merge using stemming
//   b. remove too rare ngrams
// 3. sort by frequence, considering n-1 ngrams
// 4. remove [N-1]gram collocations, when they are part of bigger choosen Ngram

import java.io.*;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.*;
import java.util.stream.Collectors;

import com.opencsv.CSVReader;
import org.apache.commons.lang3.StringUtils;
import org.apache.commons.lang3.tuple.Pair;
import org.tartarus.snowball.ext.*;

class Utils {
    private static russianStemmer stemmer = new russianStemmer();
    private static Set<String> stopWords = new HashSet<>();
    static {
        try { stopWords.addAll(Files.readAllLines(Paths.get("./russtopwords.txt"))); }
        catch (Exception e) { throw new RuntimeException(); }

        //String stopw = "а в и к c у я бы вы во да до ее её ей ею же за из об по со от то на не но он ты уж был всё его для ещё как моя сам уже что это сама свою этого которые который которых";
        String stopw = "н э";
        Arrays.stream(stopw.split(" ")).forEach(s -> { if (!s.equals("")) { stopWords.add(s); } });
    }
    private static String stem(String s) {
        stemmer.setCurrent(s);
        stemmer.stem();
        return stemmer.getCurrent();
    }

    static String stemWords(String s) {
        return String.join(" ", Arrays.stream(s.split(" ")).map(x -> stem(x)).collect(Collectors.toList()));
    }

    static boolean isStopWord(String s) {
        return stopWords.contains(s);
    }
}

class Parser {
    private ITextProvider m_Provider;
    private List<String> m_Words;
    private String m_CurrentLine;
    private int m_MaxLines;

    Parser(ITextProvider provider, int maxLines) {
        m_Provider = provider;
        m_MaxLines = (maxLines < 1) ? (Integer.MAX_VALUE - 1) : maxLines;
        m_CurrentLine = "";
    }
    // return list of words in line
    List<String> parseLine() throws IOException {
        if (m_MaxLines == 0) {
            return null;
        }
        m_Words = new ArrayList<>();
        while (true) {
            if (m_CurrentLine.equals("")) {
                m_CurrentLine = m_Provider.nextLine();
                if (m_CurrentLine == null) { return null; }
                preprocessInputLine();
            }
            else {
                handleInputLine();
                if (m_Words.size() > 0) {
                    m_MaxLines--;
                    return m_Words;
                }
            }
        }
    }

    private void preprocessInputLine() {
        m_CurrentLine = m_CurrentLine
            .replaceAll("https?://(\\w+|\\.|/)+", "")
            .replaceAll("Images\\[\\d+(,\\s\\d+)?]", "")
            .replaceAll("\\(F\\)", "")
            .replace("\"", "")
            .replace("'", "")
            .replace('ё', 'е');
    }

    private void handleInputLine() {
        int from = 0;
        int i = 0;
        while (i <= m_CurrentLine.length()) {
            while (isWordChar(i)) { i++; }
            if (from < i) {
                String word = m_CurrentLine.substring(from, i).toLowerCase();
                if (!Utils.isStopWord(word)) {
                    m_Words.add(word);
                }
            }
            if (i >= m_CurrentLine.length() - 1) { m_CurrentLine = ""; break; }
            if (!canContinueParsing(i)) {
                m_CurrentLine = m_CurrentLine.substring(i + 1);
                break;
            }
            from = ++i;
        }
    }
    private boolean canContinueParsing(int i) {
        char c = m_CurrentLine.charAt(i);
        return Character.isWhitespace(c);
    }
    private boolean isWordChar(int i) {
        if (i >= m_CurrentLine.length()) { return false; }
        char c = m_CurrentLine.charAt(i);
        return  Character.isAlphabetic(c) ||
                Character.isDigit(c);
    }
}

interface ITextProvider {
    String nextLine();
}

class CSVProvider implements ITextProvider {
    private CSVReader m_Reader;
    CSVProvider(String fileName) {
        try {
            m_Reader = new CSVReader(new FileReader(fileName));
            nextLine();
        }
        catch (Exception e) {
            throw new RuntimeException();
        }
    }
    @Override
    public String nextLine() {
        try {
            String[] line;
            return (line = m_Reader.readNext()) != null ? line[3] : null;
        }
        catch (IOException e) {
            throw new RuntimeException();
        }
    }
}

class TxtProvider implements ITextProvider {
    private BufferedReader m_Reader;
    TxtProvider(String fileName) {
        try {
            InputStream stream = new FileInputStream(fileName);
            m_Reader = new BufferedReader(new InputStreamReader(stream, "UTF-8"));
        }
        catch (Exception e) {
            throw new RuntimeException();
        }
    }
    @Override
    public String nextLine() {
        try {
            return m_Reader.readLine();
        }
        catch (IOException e) {
            System.out.print(e.getMessage());
            throw new RuntimeException();
        }
    }
}

class StringProvider implements ITextProvider {
    private String str;
    StringProvider(String s) { str = s; }
    @Override
    public String nextLine() {
        String tmp = str;
        str = null;
        return tmp;
    }
}


class NLPSolver {
    int m_MaxNgram;
    private static final int MIN_FREQ_THRESHOLD = 2;
    private static final double DROP_NPREV_COEF = 1.3;
    List<List<String> > words;

    private List<Map<String, Integer> > ngrams;
    private List<Map<String, String> > stem2Full;


    private List<Integer> needTopNgrams;
    private List<List<String> > choosenNgrams;

    NLPSolver() {
        m_MaxNgram = 3;
    }

    static void runOnDocumentsCsv(String fileName, int parserLimit) {
        // System.err.println("runOnDocumentsCsv: (" + fileName + ")");
        try (CSVReader reader = new CSVReader(new FileReader(fileName))) {
            String[] line = reader.readNext();
            for (int i = 0; i < parserLimit; i++) {
                String docText = (line = reader.readNext()) != null ? line[3] : null;
                if (docText == null || docText.length() < 280) { continue; }
                NLPSolver solver = new NLPSolver();
                solver.runOnString(docText, parserLimit);

                if (solver.hasResults()) {
                    System.out.println(line[2]);
                    solver.writeResults();
                    System.out.println();
                }
            }
        }
        catch (Exception e) {
            throw new RuntimeException();
        }
    }

    private void runOnString(String s, int parserLimit) throws IOException {
        // initialization
        Parser parser = new Parser(new StringProvider(s), parserLimit);
        run(parser);
    }
    void runOnFile(String fileName, int parserLimit) throws IOException {
        // initialization
        Parser parser = new Parser(fileName.endsWith(".csv") ?
                new CSVProvider(fileName) :
                new TxtProvider(fileName), parserLimit);
        run(parser);
    }
    void run(Parser parser) throws IOException {
        //needTopNgrams = new ArrayList<>(Arrays.asList(0, 17, 12, 8, 5, 3));
        needTopNgrams = new ArrayList<>(Arrays.asList(0, 45, 35, 25, 15, 3));

        ngrams = new ArrayList<>();
        for (int i = 0; i <= m_MaxNgram; i++) { ngrams.add(new HashMap<>()); }

        stem2Full = new ArrayList<>();
        for (int i = 0; i <= m_MaxNgram; i++) { stem2Full.add(new HashMap<>()); }

        choosenNgrams = new ArrayList<>();
        choosenNgrams.add(null);

        // start proccessing
        System.err.println("start processing");
        int totalWords = 0;
        List<String> words = parser.parseLine();
        while (words != null) {
            totalWords += words.size();
            handleWords(words);
            words = parser.parseLine();
        }
        final double totalWordsF = totalWords * 1.0;
        System.err.println("data read");

        compressNgrams();

        System.err.println("ngrams compressed");

        List<String> uniqWords = new ArrayList<>(ngrams.get(1).keySet())
                .stream().filter(s -> !s.matches("\\d{1,2}")).collect(Collectors.toList());
        uniqWords.sort((a, b) -> {
            double afreq = ngrams.get(1).get(a);
            if (a.matches("^\\d+$")) { afreq /= 10.0; }
            double bfreq = ngrams.get(1).get(b);
            if (b.matches("^\\d+$")) { bfreq /= 10.0; }
            return cmpDouble(bfreq, afreq);
        });
        choosenNgrams.add(uniqWords.subList(0, Math.min(needTopNgrams.get(1), uniqWords.size())));

        System.err.println("uniq words sorted");

        for (int i = 2; i <= m_MaxNgram; i++) {
            final int ind = i;
            List<String> sngrams = new ArrayList<>(ngrams.get(i).keySet());
            List<Pair<String, Double> > uniqNgrams = sngrams.stream()
                    .map(a -> {
                        String sleft = a.substring(0, a.lastIndexOf(" "));
                        String sright = a.substring(a.indexOf(" ") + 1, a.length());
                        double afreq = ngrams.get(ind).get(a);
                        double lfreq = ngrams.get(ind - 1).get(sleft);
                        double rfreq = ngrams.get(ind - 1).get(sright);
                        return Pair.of(a, afreq * afreq / Math.sqrt(lfreq*rfreq));
                    })
                    .collect(Collectors.toList());
            uniqNgrams.sort((a, b) -> -cmpDouble(a.getRight(), b.getRight()));
            choosenNgrams.add(uniqNgrams.subList(0, Math.min(needTopNgrams.get(i), uniqNgrams.size())).stream()
                    .map(p -> p.getLeft()).collect(Collectors.toList()));

            // filter (N-1)grams
            final int fini = i;
            final int iprev = i - 1;
            choosenNgrams.get(i).forEach(a -> {
                String sleft = a.substring(0, a.lastIndexOf(" "));
                String sright = a.substring(a.indexOf(" ") + 1, a.length());

                if (ngrams.get(iprev).get(sleft) < ngrams.get(fini).get(a) * DROP_NPREV_COEF) {
                    choosenNgrams.get(iprev).remove(sleft);
                }
                if (ngrams.get(iprev).get(sright) < ngrams.get(fini).get(a) * DROP_NPREV_COEF) {
                    choosenNgrams.get(iprev).remove(sright);
                }
            });
            System.err.println("loop for " + String.valueOf(i) + " finished.");
        }
    }

    private void handleWords(List<String> words) {
        for (int ngramLen = 1; ngramLen <= m_MaxNgram; ngramLen++) {
            for (int i = 0; i < words.size() - ngramLen + 1; i++) {
                countNgram(ngramLen, String.join(" ", words.subList(i, i + ngramLen)));
            }
        }
    }

    private void countNgram(int ngramLen, String ngram) {
        Map<String, Integer> bucket = ngrams.get(ngramLen);
        if (!bucket.containsKey(ngram)) {
            bucket.put(ngram, 1);
        }
        else {
            bucket.put(ngram, bucket.get(ngram) + 1);
        }
    }

    private void compressNgrams() {
        for (int ngrlen = 1; ngrlen <= m_MaxNgram; ngrlen++) {
            Map<String, Integer> bucket = ngrams.get(ngrlen);
            final Map<String, Set<String> > stem2Ngrams = new HashMap<>();

            bucket.keySet().forEach(s -> {
                String key = Utils.stemWords(s);
                if (!stem2Ngrams.containsKey(key)) {
                    stem2Ngrams.put(key, new HashSet<>());
                }
                stem2Ngrams.get(key).add(s);
            });

            Map<String, Integer> newNgram = new HashMap<>();

            for (Map.Entry<String, Set<String> > entry: stem2Ngrams.entrySet()) {
                String maxFreq = entry.getValue().stream().max((a, b) -> bucket.get(a) - bucket.get(b)).get();
                Integer newFreqVal = entry.getValue().stream().mapToInt(bucket::get).sum();
                if (newFreqVal >= MIN_FREQ_THRESHOLD) {
                    newNgram.put(entry.getKey(), newFreqVal);
                    stem2Full.get(ngrlen).put(entry.getKey(), maxFreq);
                }
            }
            ngrams.set(ngrlen, newNgram);
        }
    }

    boolean hasResults() {
        for (int ngrlen = m_MaxNgram; ngrlen >= 1; ngrlen--) {
            if (choosenNgrams.get(ngrlen).size() > 0) { return true; }
        }
        return false;
    }
    void writeResults() {
        for (int ngrlen = m_MaxNgram; ngrlen >= 1; ngrlen--) {
            final Map<String, String> bucket = stem2Full.get(ngrlen);
            String ngram = StringUtils.join(choosenNgrams.get(ngrlen).stream().map(bucket::get).toArray(), ',');
            if (!ngram.equals("")) {
                System.out.println('[' + ngram + ']');
            }
        }
    }

    private int cmpDouble(double a, double b) {
        if (a < b) return -1;
        if (a > b) return 1;
        return 0;
    }
}

public class Main {

    public static void main(String args[]) throws Exception
    {
        int limit = 5000;
        //String file = "wiki-rus-name.txt"; boolean documentsCsv = false;
        String file = "new_best_train_content.csv"; boolean documentsCsv = false;

        if (args.length > 0) {
            for (int i = 0; i < args.length; i++) {
                switch (args[i]) {
                    case "--csvdocs":
                        documentsCsv = true;
                        break;
                    case "-l":
                        limit = Integer.valueOf(args[++i]);
                        break;
                    default:
                        file = args[i];
                        break;
                }
            }
        }

        //String fileName = "/home/user/Documents/MLHW/train_triplets.txt.zip";
        //Solver solver = new Solver(fileName);

        //solver.run("/home/user/Documents/MLHW/new_best_train_content.csv");
        //solver.run("/home/user/Documents/MLHW/wikitext2.txt");
        //solver.run("/home/user/Documents/MLHW/dostoevsky.txt");
        //solver.run("/home/user/Documents/MLHW/my_content.txt");
        if (documentsCsv) {
            NLPSolver.runOnDocumentsCsv(file, limit);
        }
        else {
            NLPSolver solver = new NLPSolver();
            solver.runOnFile(file, limit);
            solver.writeResults();
        }
    }
}
