/**
Kirill Melentyev (c) 2013 
Стековый калькулятор
stack.h
*/
#ifndef _STACK_H_
#define _STACK_H_


typedef struct StackElement {
    struct StackElement *_next;
    int val;
} StackElement;

typedef struct {
    StackElement *top;
} Stack;


Stack stack_create();
void stack_push(Stack*,int);
int stack_size(Stack*);
int stack_pop(Stack*);
int stack_dup(Stack*);
int stack_empty(Stack*);

#endif