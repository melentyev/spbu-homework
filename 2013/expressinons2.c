﻿/**
 * Kirill Melentyev (c) 2013 
 * Разбор выражения - рекурсивный спуск
 * (с обработкой ошибок)
 */

#include <stdio.h>
#include <string.h>
#include <memory.h>
#include <stdlib.h>
#include <ctype.h>

//#define DEBUG_1
#define MAX_LEN 1000

typedef enum tokenType {
    TT_NUMBER = 0,
    TT_PLUS = 1,
    TT_MINUS = 2,
    TT_ASTERISK = 3,
    TT_SLASH = 4,
    TT_BR_OPEN = 5,
    TT_BR_CLOSE = 6,
    TT_END = (-1),
    TT_UNDEFINED = -2
} tokenType;

typedef enum __errorType {
    ERR_NONE = 0,
    ERR_DIVISION_BY_ZERO = 1,
    ERR_UNEXPECTED_TOKEN = 2,
    ERR_EXPECTED_BR_CLOSE = 3,
} errorType;

typedef struct token {
    tokenType type;
    int value;
    char* begins;
} token;

typedef struct __computationResult {
    errorType error;
    int value; 
} result;

result Factor();
result Term();
result Expression();

char *global_expression, *global_position_pointer = 0, *global_position_previous = 0, *global_position_next;
token _currentToken;

result make_result(errorType error, int value) {
     result res;
     res.error = error;
     res.value = value;
     return res;
}

void print_result(char *prefix, result r) {
    char* k;
    printf("%s", prefix);
    if(r.error == ERR_NONE) {
        printf("Result: %d\n", r.value);
    }
    else {
        printf("Error: ");
        switch (r.error) {
            case ERR_DIVISION_BY_ZERO:
                printf("division by zero.\n");
                break;
            case ERR_UNEXPECTED_TOKEN:
                printf("unexpected token.\n");
                puts(global_expression);
                for(k = global_expression; k < global_position_previous; k++) {
                    printf(" ");
                }
                printf("^\n");
                break;
            case ERR_EXPECTED_BR_CLOSE:
                printf("expected ')'\n");
                puts(global_expression);
                for(k = global_expression; k < global_position_previous; k++) {
                    printf(" ");
                }
                printf("^\n");
                break;
            default:
                printf("...\n");
        }
    }
}

token next_token() {
    char* s = global_position_pointer;
    int parsing_number = 0;
    token res;
    if(s == 0) {
        s = global_expression;
    }
    global_position_previous = s;
    
    res.type = TT_UNDEFINED;
    res.value = 0;
    while(res.type == TT_UNDEFINED) {
        if(parsing_number) {
            if(isdigit(*s) ) {
                res.value = res.value * 10 + (*s - '0');
                s++;
            }
            else {
                res.type = TT_NUMBER;
            }
        }
        else {
            if (isdigit(*s) ) {
                parsing_number = 1;
            }
            else {
                if(*s == ' ' || *s == '\t' || *s == '\n');
                else if(*s == '+') res.type = TT_PLUS;
                else if(*s == '-') res.type = TT_MINUS;
                else if(*s == '*') res.type = TT_ASTERISK;
                else if(*s == '/') res.type = TT_SLASH;
                else if(*s == '(') res.type = TT_BR_OPEN;
                else if(*s == ')') res.type = TT_BR_CLOSE;
                else if(*s == 0  ) res.type = TT_END;
                else {
                    char* k;
                    printf("Parsing error:\n");
                    puts(global_expression);
                    for(k = global_expression; k < s; k++) {
                        printf(" ");
                    }
                    printf("^\n");
                    exit(0);
                }
                s++;
            }
        }
    }
    if (res.type == TT_END) {
        s = 0;
    }                          
#ifdef DEBUG_1
    printf("get_token: type=%d value=%d\n", res.type, res.value);
#endif
    _currentToken = res;
    global_position_pointer = s;
    return res;
}

token get_token() { 
    return _currentToken;
}
result Expr() {
    result lhs = Term(), rhs;
    token op;
    if(lhs.error !=  ERR_NONE) {
        return lhs;
    }
    for (op = get_token(); op.type == TT_PLUS || op.type == TT_MINUS; op = get_token() ) {         
        next_token();
        rhs = Term();    
#ifdef DEBUG_1        
        print_result("Expr:lhs:", lhs);
        printf("Expr:op:%d\n", op.type);
        print_result("Expr:rhs:", rhs);
#endif
        if(rhs.error !=  ERR_NONE) {
            return rhs;
        }
        if(op.type == TT_PLUS) {
            lhs.value += rhs.value;
        }
        else if(op.type == TT_MINUS) {
            lhs.value -= rhs.value;
        } 
        else {
            return make_result(ERR_UNEXPECTED_TOKEN, 0);
        }
    }
    if(op.type != TT_END && op.type != TT_BR_CLOSE) {
        return make_result(ERR_UNEXPECTED_TOKEN, (int) op.type);    
    }
    else {
        return make_result(ERR_NONE, lhs.value); 
    }
}

result Term() {
    result lhs = Factor(), rhs;
    token op;
    if(lhs.error !=  ERR_NONE) {
        return lhs;
    }
    for(op = get_token(); op.type == TT_ASTERISK || op.type == TT_SLASH; op = get_token() ) {
        next_token(); 
        rhs = Factor();
#ifdef DEBUG_1
        print_result("Term:lhs:", lhs);
        printf("Term:op:%d\n", op.type);
        print_result("Term:rhs:", rhs);
#endif
        if(rhs.error != ERR_NONE) {
            return rhs;
        }
        if(op.type == TT_ASTERISK) {
            lhs.value *= rhs.value;
        }
        else if(op.type == TT_SLASH) {
            if(rhs.value != 0) {
                lhs.value /= rhs.value;
            }
            else {
                return make_result(ERR_DIVISION_BY_ZERO, 0); 
            }
        }
        else {
            return make_result(ERR_UNEXPECTED_TOKEN, 0);
        } 
    }
    return make_result(ERR_NONE, lhs.value);
}

result Factor() {
    token next = get_token();
    result res;
    if (next.type == TT_NUMBER) {
        next_token();
        return make_result(ERR_NONE, next.value);
    }
    else if(next.type == TT_BR_OPEN) {
        next_token();
        res = Expr();
        if(res.error != ERR_NONE) {
            return res;
        }
        if(get_token().type == TT_BR_CLOSE) {
            next_token();
            return make_result(ERR_NONE, res.value);
        }
        else {
            return make_result(ERR_EXPECTED_BR_CLOSE, 0);
        }
    }
    else {
        return make_result(ERR_UNEXPECTED_TOKEN, 0);
    }
}

result calculate() {
    result r;
    token t;
    r = Expr();
    if (r.error != ERR_NONE) {
        return r; 
    }
    else {
        t = get_token();
        if (t.type != TT_END) {
            return make_result(ERR_UNEXPECTED_TOKEN, t.type);
        }
        else {
            return r;
        }
    }
}

int main() {

    global_expression = (char*)malloc(sizeof(char) * MAX_LEN);
    gets(global_expression);
    next_token();
    print_result("Finaly: ", calculate());
    
    return 0;
}