#!/bin/bash

JAR=${JAR:-out/artifacts/NLPTask_jar/NLPTask.jar}

echo "Downloading test data..."

node crawl.js -d > dostoevsky.txt
node crawl.js -w --url="https://ru.wikipedia.org/wiki/%D0%9A%D0%BE%D0%BC%D0%B5%D1%82%D0%B0_%D0%93%D0%B0%D0%BB%D0%BB%D0%B5%D1%8F" > wiki-galley.txt
node crawl.js -w --url="https://ru.wikipedia.org/wiki/%D0%A0%D1%83%D1%81%D1%81%D0%BA%D0%BE%D0%B5_%D0%BB%D0%B8%D1%87%D0%BD%D0%BE%D0%B5_%D0%B8%D0%BC%D1%8F" > wiki-rus-name.txt

node crawl.js -w --url="https://ru.wikipedia.org/wiki/%D0%A0%D0%B5%D0%BB%D0%B8%D0%B3%D0%B8%D0%BE%D0%B7%D0%BD%D1%8B%D0%B5_%D0%B2%D0%B7%D0%B3%D0%BB%D1%8F%D0%B4%D1%8B_%D0%98%D1%81%D0%B0%D0%B0%D0%BA%D0%B0_%D0%9D%D1%8C%D1%8E%D1%82%D0%BE%D0%BD%D0%B0" > wiki-newton-relig.txt

mkdir -p results

TESTS=(wiki-galley.txt wiki-rus-name.txt wiki-newton-relig.txt dostoevsky.txt)

echo "Running analyzer..."
for test in "${TESTS[@]}"
do
    java -jar $JAR $test -l 0 > results/$test.out
done

test=new_best_train_content.csv; java -jar $JAR $test -l 10000 --csvdocs > results/$test.csvdocs.out
test=new_best_train_content.csv; java -jar $JAR $test -l 2000000 > results/$test.out

echo "Done.";
