using System;
using System.Linq;
using System.Collections.Generic;

namespace NetTask3
{
	internal class God : IGod
	{
		const double RandomMoneyBase = 10.0;

		const double RandomAvgMin = 1.1;
		const double RandomAvgRange = 3.8;

		const int RandomChildrenMin = 1;
		const int RandomChildrenMax = 4;

		const int MinStudentAge = 17;
		const int MaxStudentAge = 25;
		const int StudentAgeDefault = 20;

		const int ParentAgeBaseMin = 18;
		const int ParentAgeBaseMax = 36;

		Random rnd = new Random();
		List<Func<Gender, Human> > maleCreators = new List<Func<Gender, Human> >();
		List<Func<Gender, Human> > femaleCreators = new List<Func<Gender, Human> >();

		List<Human> humansList = new List<Human>();
		internal God ()
		{
			Func<Gender, Human> makeStudent = (gender) =>
				new Student(NamesHelper.RandomName(gender), RandomStudentAge(), gender, NamesHelper.RandomPatronimyc(gender));
			
			Func<Gender, Human> makeBotan = (gender) => 
				new Botan(NamesHelper.RandomName(gender), RandomStudentAge(), gender, NamesHelper.RandomPatronimyc(gender), RandomAvg ());
			
			maleCreators.Add(makeStudent);
			maleCreators.Add(makeBotan);

			maleCreators.Add((gender) => 
				new Parent(NamesHelper.RandomName(gender), RandomParentAge(), gender, RandomChildren()));
			
			maleCreators.Add((gender) => 
				new CoolParent(NamesHelper.RandomName(gender), RandomParentAge(), gender, RandomChildren(), RandomMoney()));

			femaleCreators.Add(makeStudent);
			femaleCreators.Add(makeBotan);
		}

		private Gender SelectGender() 
		{
			switch (humansList.Count)
			{
			case 0: return Gender.Male;
			case 1: return Gender.Female;
			default:
				// GET ENUM ELEMENTS NUMBER WITH REFLECTION
				int genders = Enum.GetNames(typeof(Gender)).Length;
				return rnd.Next(genders) == 0 ? Gender.Male : Gender.Female;
			}
		}

		public Human CreateHuman () => CreateHuman(SelectGender());
		public Human CreateHuman(Gender gender)
		{
			Human res = gender == Gender.Female ? 
				femaleCreators[rnd.Next(femaleCreators.Count)](gender) :
				maleCreators[rnd.Next(maleCreators.Count)](gender);
			humansList.Add(res);
			return res;
		}

		public Human CreatePair(Human h) {
			Human res = CreatePairImpl(h);
			humansList.Add(res);
			return res;
		}
		private Human CreatePairImpl(Human h) {
			if (h is Botan)
			{
				Botan botan = (Botan)h;
				return new CoolParent(NamesHelper.GenerateParentName(botan),
					RandomParentAge(botan), 
					Gender.Male, RandomChildren(), 
					(decimal)Math.Pow(RandomMoneyBase, botan.Avg));
			}
			if (h is Student)
			{
				Student student = (Student)h;
				return new Parent(NamesHelper.GenerateParentName(student),
					RandomParentAge(student), 
					Gender.Male, RandomChildren());
			}
			if (h is CoolParent) {
				CoolParent coolParent = (CoolParent)h;
				Gender g = SelectGender();
				return new Botan(NamesHelper.RandomName(g),
					RandomStudentAge(coolParent), g,
					NamesHelper.GeneratePatronimyc(coolParent.Name, g),
					Math.Log10((double)coolParent.Money));
			}

			if (h is Parent) {
				Parent parent = (Parent)h;
				Gender g = SelectGender();
				return new Student(NamesHelper.RandomName(g),
					RandomStudentAge(parent), g,
					NamesHelper.GeneratePatronimyc(parent.Name, g));
			}
			throw new ArgumentException("human");
		}

		internal decimal this[int i]
		{
			get
			{
				if ((i < 0) || (i >= humansList.Count))
				{
					throw new ArgumentOutOfRangeException();
				}
				return humansList[i] is CoolParent ? (humansList[i] as CoolParent).Money : 0;
			}
		}

		internal decimal GetTotalMoney()
		{
			decimal sum = 0;
			for (var i = 0; i < humansList.Count; i++) 
			{
				sum += this[i];
			}
			return sum;
		}

		internal Human[] GetHumans() {
			return humansList.ToArray();
		}

		// DELETE AGE CONSTANTS
		private int RandomStudentAge(Parent parent = null)
		{
			return parent != null ?
				Math.Max(MinStudentAge, parent.Age - rnd.Next(ParentAgeBaseMin, ParentAgeBaseMax)) :
				rnd.Next(MinStudentAge, MaxStudentAge);
		}

		private int RandomParentAge(Student student = null) {
			return (student != null ? student.Age : StudentAgeDefault) + rnd.Next (ParentAgeBaseMin, ParentAgeBaseMax);
		}

		private double RandomAvg() 
		{
			return RandomAvgMin + rnd.NextDouble () * RandomAvgRange;
		}

		private decimal RandomMoney() {
			return (decimal)Math.Pow(RandomMoneyBase, RandomAvg());
		}

		private int RandomChildren() {
			
			return rnd.Next(RandomChildrenMin, RandomChildrenMax);
		}
	}
}
