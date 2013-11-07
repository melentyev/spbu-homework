/**
 * Kirill Melentyev (c) 2013 
 * Стековый калькулятор
 */
  
#include <stdio.h>
#include <stdlib.h>
#include <memory.h>
#include <string.h>
#include "stack.h"

#define TRUE 1
#define FALSE 0 

int run() {
    int a, b;
    FILE *in = stdin; 
    int done = FALSE, first;
    stack s = stack_create();
    char cmd[20];
    do {
        fgets(cmd, 20, in);
        if(strlen(cmd) == 1) continue;
        cmd[strlen(cmd) - 1] = 0;
        if(strcmp(cmd, "exit") == 0) {
            done = TRUE;
        }
        else if(strcmp(cmd, "pop") == 0) {
            if(!stack_empty(&s) ) {
                printf("%d\n", stack_pop(&s));
            }
            else {
                printf("Error: stack empty\n");
                continue;
            }
        }
        else if(strcmp(cmd, "dup") == 0) {
            if(!stack_dup(&s) ) {
                printf("Error: stack empty\n");
                continue;
            }
        }
        else if(strlen(cmd) == 1 && (cmd[0] == '+' || cmd[0] == '-' || cmd[0] == '*' || cmd[0] == '/') ) { 
            if (stack_size(&s) < 2) {
                printf("Error: not enough operands\n");
                continue;    
            }
            b = stack_pop(&s), a = stack_pop(&s);
            switch(cmd[0]) {
            case '+':
                stack_push(&s, a + b);
                break;
            case '-':
                stack_push(&s, a - b);
                break;
            case '*':
                stack_push(&s, a + b);
                break;
            case '/':
                if (b != 0) {
                    stack_push(&s, a / b);
                }
                else {
                    printf("Error: division by zero\n");
                    return FALSE;
                }
                break;
            }
            printf("Operation result: %d\n", s.top->val);
        }
        else if(sscanf(cmd, "%d", &a) > 0) {
            stack_push(&s, a);
        }
        else {
            printf("Error: unknown command\n");
            continue;
        }
    } while(!done);
    return TRUE;
}

int main() {
    if(!run()) { // if error occured
        printf("Press any key to exit...\n");
        getc(stdin);
    }
    return 0;
}
    