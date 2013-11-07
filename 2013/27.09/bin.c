/**
 * Kirill Melentyev (c) 2013 
 * Битовые операции
 */
  
#include <stdio.h>
#include <stdlib.h>
#include <memory.h>
#include <time.h>  

#define TRUE 1
#define FALSE 0 

void intbits(int x, int leadingZeros) {    
    int printed = FALSE;
    int val, i;
    for (i = 31; i >= 0; i--) {
        if (val =  ( (x >> i) & 1) ) {
            printed = TRUE;
            printf("%c", '0' + val);
        }
        else {
            if (printed || leadingZeros) {
                printf("%c", '0' + val);
            }
        }
    }
    if(!printed) {
        printf("0");
    }
    printf("\n");
}

void endianness() {
    int a = 1;
    if ( *((char*)&a) != (char)0 ) {
        printf("Bytes order: little endian\n");
    }
    else {
        printf("Bytes order: big endian\n");
    }
}

void check_complement(int n) {
    printf("Checking complement ( -%d == ( (~%d) + 1) ): %s\n", n, n, (-n == ( (~n) + 1) ? "Yes" : "No") );
}

void run() {
    int n;
    printf("Enter number: \n");
    scanf("%d", &n);
    printf("With leading zeros:\n");
    intbits(n, TRUE);
    printf("Without leading zeros:\n");
    intbits(n, FALSE);
    check_complement(n);
    endianness();
    system("pause");
}

int main() {
    run();
    return 0;
}
    