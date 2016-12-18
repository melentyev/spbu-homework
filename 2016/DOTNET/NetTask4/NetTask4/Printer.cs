using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTask4
{
    internal enum PrintType { Info, Parent, Child, Exception }
    internal class Printer
    {
        internal static void Write(PrintType pt, string s)
        {
            ConsoleColor prev = Console.BackgroundColor;
            switch (pt)
            {
                case PrintType.Exception: Console.BackgroundColor = ConsoleColor.DarkRed; break;
                case PrintType.Child: Console.BackgroundColor = ConsoleColor.DarkBlue; break;
                default: break;
            }
            Console.WriteLine(s);
            Console.BackgroundColor = prev;
        }
    }
}
