/**
 * Kirill Melentyev (c) 2013 
 *
 *
 */

#include <stdio.h>
#include <stdlib.h>
#include <memory.h>    
#include <string.h>

#define TRUE 1
#define FALSE 0
#define DEFAULT_CHUNK_SIZE 2

#define RED 0
#define BLACK 1

int next_card(int *value, int *color) {
    char sCard[10], sType[10];
    scanf("%s%s", sCard, sType);
    if (strcmp(sCard, "Jack") == 0 || strcmp(sCard, "Queen") == 0 || strcmp(sCard, "King") == 0) {
        *value = 10;       
    }
    else if(strcmp(sCard, "Ace") == 0) {
        *value = 11;
    }
    else {
        sscanf(sCard, "%d", value);
    }
    if (strcmp(sType, "Spades") == 0 || strcmp(sType, "Clubs") == 0) {
        *color = RED;
    }
    else {
        *color = BLACK;
    }
}

int calc_score(int sum, int colors) {
    
}

void run(FILE *input) {
    int sum = 0, color1 = 0, color2 = 0, value, color;
    
    while (true) {
        next_card(&value, &color);
        if (color == -1 || color1 == color) {
            color1 = color;
        }
        else { 
            color2 = color;
        }
        if (calc_score(sum, (color1 + color2 > 1) ) < 21) {
        
        }
    }
}

int main(int argc, char **argv) {
    int k = 2*- -3;
    if(argc > 1) {
        FILE *input = fopen(argv[1], "r");
        run(input);
    }
    return 0;
}
