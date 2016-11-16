using System;

namespace NetTask3
{
	internal class Parent : Human
	{
		internal int Children { get; }

		public Parent (string name, int age, Gender gender, int children)
			: base(name, age, gender)
		{
			Children = children;
		}

		internal override void Print() {
			Console.Write("Parent    ({0}, {1}, {2} years, {3} children)", Name, Gender, Age, Children);
		}
	}
}

