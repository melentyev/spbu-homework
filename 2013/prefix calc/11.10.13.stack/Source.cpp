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

	n = strlen(pri);
	while (str[i] != '\0')
	{
		while (str[i] != ' ' && str[i] != ',' && str[i] != '\0') {
			s[j] = str[i];
			len++;
			i++;
			j++;
		}
        s[len] = 0;
		if ( strstr(s, pri) != NULL && strstr(s, pri) == s)
		for (j = 0; j < len; j++)
			printf("%c", s[j]);
		printf("\n");
		i += !!str[i];
		j = 0;
		len = 0;
	}

getch();
}