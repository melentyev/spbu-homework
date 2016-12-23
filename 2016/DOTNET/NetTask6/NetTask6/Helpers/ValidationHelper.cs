using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTask6.Helpers
{
    internal sealed class ValidationHelper
    {
        internal const int LumiereFirstFilmDate = 1895;
        internal static bool ValidateYear(int year)
        {
            return year == 0 || (year >= LumiereFirstFilmDate && year <= DateTime.Now.Year);
        }
        // Russian letters, digits, some symbols
        internal static bool ValidatePlainText(string s)
        {
            return s == null || s.All(c =>
                Char.IsWhiteSpace(c) || Char.IsDigit(c) ||
                c == '-' || c == ',' || c == '.' ||
                (c >= 'а' && c <= 'я') || c == 'ё' ||
                (c >= 'А' && c <= 'Я') || c == 'Ё');
        }
    }
}
