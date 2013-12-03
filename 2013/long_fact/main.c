/**
 * Kirill Melentyev (c) 2013 
 *
 * Polinom
 */

#include <stdio.h>
#include <stdlib.h>
#include <memory.h>    
#include <string.h>

#define TRUE 1
#define FALSE 0                  

#define MAX_LEN 20
#define MOD ((int) 1e8)
// Храним по восемь цифр

void print_result(int *bigint);

void true_solution(int n) {
    int bigint[MAX_LEN], i, j, mult, acc = 0;
    long long buf;
    for (i = 0; i < MAX_LEN; i++) {
        bigint[i] = 0;
    }
    bigint[0] = 1;
    for (mult = 2; mult <= n; mult++) {
        for(j = 0; j < MAX_LEN; j++) {
            buf = ( (long long) bigint[j] * mult + acc);
            bigint[j] = buf % MOD;
            acc = buf / MOD;
        }
    }
    print_result(bigint);   
}

void print_result(int *bigint) {
    int i, printed = 0;
    for (i = MAX_LEN - 1; i >= 0; i--) {
        if (bigint[i] != 0) {
            if (printed) {
                printf("%08d", bigint[i]);
            }
            else {
                printf("%d", bigint[i]);
            }
            printed = 1;
        }
        else if(printed) {
            printf("%08d", bigint[i]);
        }
    }
    printf("\n");
}
  
void cheat_solution(int n) {
    char result[1000], rhs[10], buffer[1000], cmd[1000];
    int i;
    strcpy(buffer, "1"); 
    sprintf(cmd, "python -c \"print (1 ", buffer, rhs);
    for(i = 1; i < n; i++) {
        sprintf(rhs, " * %d", i + 1);                                  
        strcat(cmd, rhs);
    }
    strcat(cmd, ") \"");
    //puts(cmd);
                      
    FILE *fp = _popen(cmd, "r");
    fgets(buffer, 1000, fp);
    buffer[strlen(buffer) - 1] = 0;
    puts(buffer); 
}

int main(int argc, char **argv) {
    int n;
    scanf("%d", &n);   
    true_solution(n);
    printf("check:\n");
    cheat_solution(n);
    return 0;
}
          
/*
struct BigInt {
    int len;
    int *data;
};

void BigInt_multiply_int(BigInt lhs, int rhs, BigInt result) {
    
}

void BigInt_make(int n) {
    BigInt a;
    a.len = 1;
    a.data = (int*)malloc(sizeof(int) * 1);
    a.data[0] = n;
    return a;
}

void BigInt_free(BigInt a) {
    free(a->data);
    a->data = NULL;
}

void BigInt_print(BigInt a) {
    
}
*/