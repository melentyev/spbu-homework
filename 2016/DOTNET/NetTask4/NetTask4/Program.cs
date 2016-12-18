using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTask4
{
    internal sealed class Program
    {
        internal sealed class Static
        {
            public const string EqualGenderGenerated = "Error: random humans of same gender generated!";
            public const string Sorry7Day = "Sorry, I don't work on 7th day of week.";
            public const string PromptAction = "Press enter to generate couple (Q or F10 to exit):";
            public const string NotLiked = "Not liked.";
        }

        private static ConsoleKey[] ExitKeys { get; } = { ConsoleKey.F10, ConsoleKey.Q };
        private static ConsoleKey ContinueKey { get; } = ConsoleKey.Enter;

        internal static void Main(string[] args)
        {
            var day = DateTime.Now.DayOfWeek;
            if (day == DayOfWeek.Sunday)
            {
                Printer.Write(PrintType.Info, Static.Sorry7Day);
                return;
            }

            IGod god = new God();

            while (true)
            {
                if (!PromptUser()) { break; }
                try
                {
                    var first = god.CreateHuman();
                    var second = god.CreateHuman();
                    Printer.Write(PrintType.Parent, first.Representation);
                    Printer.Write(PrintType.Parent, second.Representation);

                    var child = god.Couple(first, second);
                    if (child != null)
                    {
                        Printer.Write(PrintType.Child, child.Representation);
                    }
                    else
                    {
                        Printer.Write(PrintType.Exception, Static.NotLiked);
                    }
                }
                catch (EqualGenderException e)
                {
                    Printer.Write(PrintType.Exception, Static.EqualGenderGenerated);
                }
                Console.WriteLine();
                
            }
        }

        private static bool PromptUser()
        {
            Printer.Write(PrintType.Info, Static.PromptAction);
            var key = Console.ReadKey(true).Key;
            while (key != ConsoleKey.F10 && key != ConsoleKey.Q && key != ConsoleKey.Enter)
            {
                key = Console.ReadKey(true).Key;
            }
            return key == ConsoleKey.Enter;
        }
    }
}
