using System;

namespace NetTask3
{
	internal class CoolParent : Parent
	{
		internal decimal Money { get; }

		internal CoolParent(string name, int age, Gender gender, int children, decimal money)
			: base(name, age, gender, children)
		{
			Money = money;
		}

		internal override void Print() {
			ConsoleColor initialBack = Console.BackgroundColor;
			ConsoleColor initialFore = Console.ForegroundColor;

			Console.Write("CoolParent({0}, {1}, {2} years, {3} children, ", Name, Gender, Age, Children);
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.BackgroundColor = ConsoleColor.DarkBlue;
			Console.Write("${0:F2}", Money);
			Console.BackgroundColor = initialBack;
			Console.ForegroundColor = initialFore;

			Console.Write(")");
		}
	}
}

