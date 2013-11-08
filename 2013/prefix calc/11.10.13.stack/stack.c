/**
Kirill Melentyev (c) 2013 
стековый калькулятор
stack.c
*/

#include <memory.h>
#include <stdlib.h>
#include "stack.h"

Stack stack_create() {
    Stack c;
    c.top = NULL;
    return c;
}

void stack_push(Stack *c, int val) {
    StackElement *el = (StackElement*)malloc(sizeof(StackElement));
    el->val = val;
    el->_next = c->top;
    c->top = el;
}

int stack_size(Stack *c) {
    int res = 0;
    StackElement *ptr = c->top;
    while(ptr != NULL) {
        ptr = ptr->_next;
        res++;
    }
    return res;
}

int stack_pop(Stack *c) {
    StackElement *ptr = c->top;
    int val = c->top->val;
    c->top = c->top->_next;
    free(ptr);
    return val;
}

int stack_empty(Stack *c) {
    return (c->top == NULL);
}

int stack_dup(Stack *c) {
    return (!stack_empty(c) ? stack_push(c, c->top->val), 1 : 0);
}