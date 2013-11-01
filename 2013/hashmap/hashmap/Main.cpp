#include <cstdio>
#include <cstring>
#include <windows.h>
#include "HashMap.h"
#include "Helpers.h"

using namespace MyHelpers;

int main() {
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);
    FILE * fin = fopen("book1.txt", "rb");
    int c;
    HashMap<String, int, StringHash, StringComp> hm;
    char *word = new char[100], wordlen = 0;
    do {
        c = getc(fin);
        if(isAlpha(c) ) {
            word[wordlen++] = toLower(c);    
        }
        else if(wordlen > 0) {
            word[wordlen] = 0;
            int *cnt = hm.find(String(word) );
            hm.insert(String(word), (cnt == NULL ? 1 : *cnt + 1) );
            wordlen = 0;
        }
    }
    while(hm.size() < 100000 && c != EOF);
    delete word;
    int cnt = 0;
    hm.iter([&cnt](String &k, int v) -> void { printf("%s: %d\n", k.c_str(), v); cnt++; });
    printf("%d\n", cnt);
    system("pause");
    return 0;
}