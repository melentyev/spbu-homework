#include <stdio.h>
#include <conio.h>
#include <string.h>
#include <locale>

int main()
{
    setlocale(LC_CTYPE, "Russian");
    char str[80], pri[10], s[20];
    int i = 0, j = 0, n = 0, k = 0, ch = 0, len = 0;

    puts("Введите строку:");
    gets(str);
    puts("Введите приставку:");
    gets(pri);
    printf("Слова с приставкой %s:\n", pri);

    while (str[i] != '\0')
    {
        while (str[i] != ' ' && str[i] != ',' && str[i] != '\0') {
            s[j] = str[i];
            len++;
            i++;
            j++;
        }
        if (strstr(s, pri) != NULL)
        for (j = 0; j < len; j++)
            printf("%s", s[j]);
        printf("\n");
        i++;
        j = 0;
        len = 0;
    }

getch();
}