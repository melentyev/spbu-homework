using System;
using System.Text;
using System.IO;

namespace NetTask3
{
	class Static {
		public const string Sorry7Day = "Sorry, I don't work on 7th day of week.";
		public const string PromptHumansCount = "Enter humans count (must be positive integer <= 10):";
		public const string PromptHumansCountAgain = "Enter humans count (must be positive integer <= 10) again:";
	}

	class MainClass
	{
		internal static void Main (string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			var day = DateTime.Now.DayOfWeek;
			if (day == DayOfWeek.Sunday)
			{
				Console.WriteLine(Static.Sorry7Day);
				return;
			}

			Console.WriteLine(Static.PromptHumansCount);
			int humansCount = 0;
			bool parsed = Int32.TryParse(Console.ReadLine(), out humansCount);
			while (!parsed || humansCount < 1 || humansCount > 10)
			{
				Console.WriteLine(Static.PromptHumansCountAgain);
				parsed = int.TryParse(Console.ReadLine(), out humansCount);
			}
			var god = new God();

			for (int i = 0; i < humansCount; i++)
			{
				var human = god.CreateHuman();
				PrintLine(human);
				Console.WriteLine("");
			}
			MakePairs(god);
			DumpTotalMoney(god);
		}
			
		private static void MakePairs(God god) 
		{
			Human[] humans = god.GetHumans();
			Console.SetCursorPosition(0, Console.CursorTop - humans.Length * 2 + 1);
			Console.BackgroundColor = ConsoleColor.DarkRed;
			foreach (Human human in humans) {
				Human pair = god.CreatePair(human);
				pair.Print();
				Console.SetCursorPosition(0, Console.CursorTop + 2);
			}
		}

		private static void PrintLine(Human human) {
			Console.BackgroundColor = ConsoleColor.Black;
			human.Print();
			Console.WriteLine("");
		}

		private static void DumpTotalMoney(God god)
		{
			string outputPath = "total-money";
			File.WriteAllText(outputPath, god.GetTotalMoney().ToString());
		}
	}
}
