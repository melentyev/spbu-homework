#include "common.h"

extern int globalLineNumber, globalLinePosition;

int allocatingBalance = 0;

void printErrorPosition() 
{
    printf("Parsing stopped at: line %d, position: %d\n", globalLineNumber, globalLinePosition);
}

void error(ErrorType type)
{
    switch (type)
    {
    case ET_DIVISION_BY_ZERO:
        puts("ET_DIVISION_BY_ZERO");
        break;
    case ET_CONST_ASSIGN:
        puts("ET_CONST_ASSIGN");
        printErrorPosition();
        break;
    case ET_UNEXPECTED_TOKEN:
        puts("Unexpected token");
        printErrorPosition();
        break;
    case ET_UNEXPECTED_EOF:
        puts("Unexpected end of file");
        printErrorPosition();
        break;
    case ET_RUNTIME_ERROR:
        puts("ET_RUNTIME_ERROR");
        break;
    case ET_EXPECTED_REGISTER:
        puts("Expected register { r1, r2, r3, r4 }");
        printErrorPosition();
        break;
    case ET_EXPECTED_BRACKET_CLOSE:
        puts("Expected ']'");
        printErrorPosition();
        break;
    case ET_UNDEFINED_LABEL:
        puts("ET_UNDEFINED_LABEL");
        break;
    }
    system("pause");
    exit(0);
}

#ifdef DEBUG
#undef malloc
#undef realloc
#undef free

void *my_alloc(size_t size) 
{
    allocatingBalance++;
    return malloc(size);
}

void my_free(void *ptr) 
{
    allocatingBalance--;
    free(ptr);
}

void *my_realloc(void *ptr, size_t size) 
{
    if (!ptr) 
    {
        allocatingBalance++;
    }
    return realloc(ptr, size);
}

#define malloc __dont_use_std_allocation_in_debug__
#define realloc __dont_use_std_allocation_in_debug__
#define free __dont_use_std_allocation_in_debug__
#endif