/**
 * Kirill Melentyev (c) 2013 
 * Стековый калькулятор
 */

#include <memory.h>
#include <stdlib.h>
#include "stack.h"

stack stack_create() {
    stack c;
    c.top = NULL;
    return c;
}

void stack_push(stack *c, int val) {
    stackElement *el = (stackElement*)malloc(sizeof(stackElement));
    el->val = val;
    el->_next = c->top;
    c->top = el;
}

int stack_size(stack *c) {
    int res = 0;
    stackElement *ptr = c->top;
    while(ptr != NULL) {
        ptr = ptr->_next;
        res++;
    }
    return res;
}

int stack_pop(stack *c) {
    stackElement *ptr = c->top;
    int val = c->top->val;
    c->top = c->top->_next;
    free(ptr);
    return val;
}

int stack_empty(stack *c) {
    return (c->top == NULL);
}

int stack_dup(stack *c) {
    return (!stack_empty(c) ? stack_push(c, c->top->val), 1 : 0);
}