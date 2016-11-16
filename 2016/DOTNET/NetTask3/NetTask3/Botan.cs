using System;

namespace NetTask3
{
	internal class Botan : Student
	{
		internal double Avg { get; }

		internal Botan(string name, int age, Gender gender, string patronymic, double avg)
			: base(name, age, gender, patronymic)
		{
			if (avg < 1 && avg > 5)
			{
				throw new ArgumentException("Invalid avg");
			}
			Avg = avg;
		}

		internal override void Print() {
			Console.Write("Botan     ({0} {1}, {2}, {3} years, {4:F2} score)", Name, Patronymic, Gender, Age, Avg);
		}
	}
}
