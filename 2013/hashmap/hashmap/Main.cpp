#include <cstring>
#include <windows.h>
#include "HashMap.h"
#include <iostream>
#include <fstream>
#include <cctype>
using namespace std;

class StrHash {
    static const unsigned int _mult = 3373, _mod = (int)1e9 + 7;
public:
    unsigned int operator()(const char *s) {
        long long sum = 0;
        long long power = 1;
        for (; *s; s++) {
            sum = (sum + (*s) * power) % _mod;
            power = (power * _mult) % _mod;
        }
        return (unsigned int)sum;
    }
};

class StrComp {
public:
    int operator()(char *s1, char *s2) {
        return strcmp(s1, s2) == 0;
    }
};

bool isAlpha(char c) {
    static char smallRussian[] = "éöóêåíãøùçõúôûâàïğîëäæıÿ÷ñìèòüáş¸";
    static char bigRussian[] = "ÉÖÓÊÅÍÃØÙÇÕÚÔÛÂÀÏĞÎËÄÆİß×ÑÌÈÒÜÁŞ¨";
    static int len = strlen(smallRussian);
    for(int i = 0; i < len; i++) {
        if(smallRussian[i] == c || bigRussian[i] == c) {
            return true;
        }
    }
    return false;
}

char toLower(char c) {
    static char smallRussian[] = "éöóêåíãøùçõúôûâàïğîëäæıÿ÷ñìèòüáş¸";
    static char bigRussian[] = "ÉÖÓÊÅÍÃØÙÇÕÚÔÛÂÀÏĞÎËÄÆİß×ÑÌÈÒÜÁŞ¨";
    static int len = strlen(smallRussian);
    for(int i = 0; i < len; i++) {
        if(bigRussian[i] == c) {
            return smallRussian[i];
        }
    }
    return c;
}

void run() {
    ifstream fin("book1.txt", ios_base::in | ios_base::binary);
    char c;
    HashMap<char*, int, StrHash, StrComp> hm;
    char *word = new char[100], wordlen = 0, *newWord;
    fin.read(&c, 1);
    do {
        if (isAlpha(c) ) {
            word[wordlen++] = toLower(c);    
        }
        else if(wordlen > 0) {
            word[wordlen] = 0;
            newWord = new char[wordlen + 2];
            strcpy(newWord, word);
            hm[newWord]++;
            wordlen = 0;
        }
        fin.read(&c, 1);
    }
    while(hm.size() < 1000000 && !fin.eof() );
    delete word;
    int cnt = 0;
    std::pair<int, char*> *ar = new std::pair<int, char*>[hm.size()];
    hm.iter([&ar, &cnt](char* k, int v) -> void { ar[cnt++] = std::make_pair(v, k); });
    sort(ar, ar + cnt);
    cout << "Âñåãî ñëîâ:" << hm.size() << endl;
    cout << "Ñàìûå ğåäêèå ñëîâà: " << endl;
    for (int i = 0; i < 10; i++) {
        cout <<  ar[i].first << " - " << ar[i].second << endl; 
    }
    cout << "Ñàìûå ÷àñòûå ñëîâà: " << endl;
    for(int i = cnt - 1; i >= cnt - 10; i--) {
        cout <<  ar[i].first << " - " << ar[i].second << endl;
    }
    system("pause");
}

int main() {
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);
    run();
    return 0;
}