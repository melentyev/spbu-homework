#ifndef _STACK_H_
#define _STACK_H_


typedef struct stackElement {
    struct stackElement *_next;
    int val;
} stackElement;

typedef struct stack {
    struct stackElement *top;
} stack;


stack stack_create();
void stack_push(stack *c, int val);
int stack_size(stack *c);
int stack_pop(stack *c);
int stack_dup(stack *c);
int stack_empty(stack *c);

#endif