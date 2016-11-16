using System;

namespace NetTask3
{
	internal class NamesHelper
	{
		private const string malePatronymicSuffix = "ович";
	 	private const string femalePatronymicSuffix = "овна";

		private static string[] maleNames = { "Иван", "Петр", "Макар", "Захар" };
		private static string[] femaleNames = { "Маша", "Даша", "Глаша", "Глафира", "Дуня" };

		private static Random rnd = new Random();

		internal static string RandomName(Gender gender) 
		{
			switch (gender) 
			{
			case Gender.Male: return maleNames[rnd.Next(maleNames.Length)];
			case Gender.Female: return femaleNames[rnd.Next(femaleNames.Length)];
			default: throw new ArgumentException("gender");
			}
		}

		internal static string RandomPatronimyc(Gender gender) 
		{
			string suffix = GenderNameSuffix(gender);
			return maleNames[rnd.Next(maleNames.Length)] + suffix;
		}

		internal static string GenderNameSuffix(Gender gender)
		{
			return gender == Gender.Male ? malePatronymicSuffix : femalePatronymicSuffix;
		}

		internal static string GenerateParentName(Student student)
		{
			if (student.Gender == Gender.Male && student.Patronymic.EndsWith(malePatronymicSuffix)) 
			{
				return student.Patronymic.Substring(0, student.Patronymic.Length - malePatronymicSuffix.Length);
			}
			else if (student.Gender == Gender.Female && student.Patronymic.EndsWith(femalePatronymicSuffix)) 
			{
				return student.Patronymic.Substring(0, student.Patronymic.Length - femalePatronymicSuffix.Length);
			}
			return student.Patronymic;
		}

		internal static string GeneratePatronimyc(string parentName, Gender childGender) 
		{
			return parentName + GenderNameSuffix(childGender);
		}
	}
}

