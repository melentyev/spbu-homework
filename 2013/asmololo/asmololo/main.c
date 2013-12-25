/**
Kirill Melentyev (c) 2013 
interpreter
*/

#include <stdlib.h>
#include "common.h"

void printUsage() 
{
    printf("Usage: asmololo <file>.lolo\n");
}

void run(FILE *in)
{
    int result;
    interpreterInit();
    parserInit(in);
    parseInput();
    linkProgram();
    result = runProgram();
    printf("Calculation result: %d\n", result);
}

#ifdef DEBUG
extern int allocatingBalance;
#endif


int main(int argc, char **argv)
{
    //int i;
    FILE *in = NULL;
    if (argc < 2) 
    {
        printUsage();
    }
    else
    {
        in = fopen(argv[1], "r");
        if (in)
        {
            run(in);
        }
        else
        {
            printf("Source not found.\n");
        }
    }
    releaseMemory();
#ifdef DEBUG
    printf("allocatingBalance: %d\n", allocatingBalance);
#endif
    system("pause");
    return 0;
}
