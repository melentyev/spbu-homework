using System;

namespace NetTask3
{
	internal abstract class Human
	{
		internal string Name { get; }
		internal int Age { get; }
		internal Gender Gender { get; }

		internal Human (string name, int age, Gender gender)
		{
			Name = name;
			Age = age;
			Gender = gender;
		}

		internal abstract void Print();
	}
}

