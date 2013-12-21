#pragma once

#include <stdio.h>
#include <string.h>
#include <memory.h>
#include <stdlib.h>
#include <ctype.h>

#define REGISTERS_COUNT 4
#define KEYWORDS_COUNT 14
#define MEMORY_SIZE (1 << 20)

// memory leaks catching
//#define DEBUG

#ifdef DEBUG
#define ALLOCATE my_alloc
#define FREE my_free
#define REALLOCATE my_realloc
#define malloc __dont_use_std_allocation_in_debug__
#define realloc __dont_use_std_allocation_in_debug__
#define free __dont_use_std_allocation_in_debug__
#else
#define ALLOCATE malloc
#define FREE free
#define REALLOCATE realloc
#endif

typedef enum TokenType
{
    TT_UNDEFINED,
    TT_EOF,
    TT_EOL,
    TT_NUMBER,
    TT_COLON,
    TT_COMMA,
    TT_WORD,
    TT_MINUS,
    TT_SEMICOLON,
    TT_BRACKET_OPEN,
    TT_BRACKET_CLOSE
} TokenType;

typedef enum CommandType
{
    CT_ADD,
    CT_SUB,
    CT_MUL,
    CT_DIV,
    CT_MOD,
    CT_MOV,
    CT_JMP,
    CT_JE,
    CT_JNE,
    CT_JL,
    CT_JNL,
    CT_JG,
    CT_JNG,
    CT_HLT
} CommandType;

typedef enum Register
{
    R_R1 = 0,
    R_R2 = 1,
    R_R3 = 2,
    R_R4 = 3
} Register;

typedef enum ArgType
{
    AT_REGISTER,
    AT_CONST,
    AT_ADDRESS,
    AT_LABEL
} ArgType;

typedef enum ErrorType
{
    ET_CONST_ASSIGN,
    ET_DIVISION_BY_ZERO,
    ET_UNEXPECTED_TOKEN,
    ET_UNEXPECTED_EOF,
    ET_RUNTIME_ERROR,
    ET_EXPECTED_REGISTER,
    ET_EXPECTED_BRACKET_CLOSE,
    ET_UNDEFINED_LABEL
} ErrorType;

typedef struct Token
{
    TokenType type;
    union
    {
        char *str;
        int _int;
    } value;
} Token;

typedef struct CommandArg
{
    ArgType type;
    union
    {
        Register r;
        int _const;
        struct CommandArg *addr;
        char *label;
        int mappedLabel;
    } argv;
} CommandArg;

typedef struct CommandArgs
{
    CommandArg arg1;
    CommandArg arg2;
} CommandArgs;

typedef struct Command
{
    CommandType type;
    CommandArgs args;
} Command;

typedef struct Label
{
    char *name;
    int commandNumber;
} Label;

typedef struct InterpreterState
{
     int halt, result, commandNumber;
} InterpreterState;

void interpreterInit();
void parserInit(FILE*);
void parseInput();
void linkProgram();
int runProgram();
void error(ErrorType);
void *my_alloc(size_t);
void *my_realloc(void*, size_t);
void my_free(void*);
void releaseMemory();