using System;

namespace NetTask4
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

        internal static string GenderNameSuffix(Gender gender)
        {
            return gender == Gender.Male ? malePatronymicSuffix : femalePatronymicSuffix;
        }

        internal static string GeneratePatronimyc(string parentName, Gender childGender)
        {
            return parentName + GenderNameSuffix(childGender);
        }
    }
}
