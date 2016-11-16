using System;

namespace NetTask3
{
	internal class Student : Human
	{
		internal string Patronymic { get; }

		public Student (string name, int age, Gender gender, string patronymic)
			: base(name, age, gender)
		{
			Patronymic = patronymic;
		}

		internal override void Print() {
			Console.Write("Student   ({0} {1}, {2}, {3} years)", Name, Patronymic, Gender, Age);
		}
	}
}

