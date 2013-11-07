#pragma once
#include <cctype>
#include <cstring>

namespace MyHelpers {

    class StringHash;
    class StrHash;
    class StringComp;
    class StrComp;

    template <class T>
    class AddressHash {
        static const unsigned int _mult = 3373, _mod = (int)1e9 + 7;
    public:
        unsigned int operator()(const T &v) {
            return (unsigned int) &v;
        }
    };

    /**
     *
     */
    class String {
        char *s;
    public:
        String(int n = 0) {
            s = new char[n + 1];
        }
        String(char *_s) {
            s = new char[strlen(_s) + 1];
            strcpy(s, _s); 
        }
        String(const String &_s) {
            s = new char[_s.length() + 1];
            strcpy(s, _s.s);
        }
        String operator=(const String &_s) {
            delete[] s;
            s = new char[_s.length() + 1];
            strcpy(s, _s.s);
            return String(_s);
        }
        ~String() {
            delete[] s;
        }
        const char *c_str() const {
            return s;
        }
        unsigned int length() const {
            return strlen(s);
        }
        bool operator < (const String &s) const {
            return false;
        }
        friend StringHash;
        friend StringComp;
    };

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

    class StringHash {
        static const unsigned int _mult = 3373, _mod = (int)1e9 + 7;
    public:
        unsigned int operator()(const String &s) {
            long long sum = 0;
            long long power = 1;
            char *ptr = s.s;
            for (; *ptr; ptr++) {
                sum = (sum + (*ptr) * power) % _mod;
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

    class StringComp {
    public:
        int operator()(const String &s1, const String &s2) {
            return strcmp(s1.s, s2.s) == 0;
        }
    };

    bool isAlpha(char c) {
        static char smallRussian[] = "éöóêåíãøùçõúôûâàïðîëäæýÿ÷ñìèòüáþ¸";
        static char bigRussian[] = "ÉÖÓÊÅÍÃØÙÇÕÚÔÛÂÀÏÐÎËÄÆÝß×ÑÌÈÒÜÁÞ¨";
        static int len = strlen(smallRussian);
        for(int i = 0; i < len; i++) {
            if(smallRussian[i] == c || bigRussian[i] == c) {
                return true;
            }
        }
        return false;
    }

    char toLower(char c) {
        static char smallRussian[] = "éöóêåíãøùçõúôûâàïðîëäæýÿ÷ñìèòüáþ¸";
        static char bigRussian[] = "ÉÖÓÊÅÍÃØÙÇÕÚÔÛÂÀÏÐÎËÄÆÝß×ÑÌÈÒÜÁÞ¨";
        static int len = strlen(smallRussian);
        for(int i = 0; i < len; i++) {
            if(bigRussian[i] == c) {
                return smallRussian[i];
            }
        }
        return c;
    }
}