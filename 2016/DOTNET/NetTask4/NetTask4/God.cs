using System;
using System.Linq;
using System.Collections.Generic;

namespace NetTask4
{
    internal sealed class God : IGod
    {
        const string MyNamespace = "NetTask4";
        const string MyAssembly = "NetTask4";
        const string PatronymicPropertyName = "Patronymic";

        Random rnd = new Random();
        
        List<Func<Human>> creators = new List<Func<Human>>();

        List<Human> humansList = new List<Human>();
        internal God()
        {
            creators.Add(() => new Student(NamesHelper.RandomName(Gender.Male)));
            creators.Add(() => new Botan(NamesHelper.RandomName(Gender.Male)));
            creators.Add(() => new Girl(NamesHelper.RandomName(Gender.Female)));
            creators.Add(() => new PrettyGirl(NamesHelper.RandomName(Gender.Female)));
            creators.Add(() => new SmartGirl(NamesHelper.RandomName(Gender.Female)));
        }

        public Human CreateHuman()
        {
            return creators[rnd.Next(creators.Count)]();
        }

        public IHasName Couple(Human first, Human second)
        {
            if (first == null || second == null)
            {
                throw new ArgumentNullException();
            }
            if (first.Gender == second.Gender)
            {
                throw new EqualGenderException();
            }
            List<Human> humans = new List<Human>(new Human[] { first, second });
            string[] classes = humans.Select(h => h.GetType().Name).ToArray();
            Func<int, string> otherClass = (i) => classes[(i + 1) % classes.Length];
            var liked = new bool[] { false, false };
            string childType = null;
            for (int i = 0; i < humans.Count; i++)
            {
                var enumerator = new CoupleAttributeEnumerator(humans[i]);
                while (enumerator.MoveNext())
                {
                    CoupleAttribute attr = enumerator.Current;
                    if (attr.Pair == otherClass(i))
                    {
                        childType = attr.ChildType;
                        liked[i] = rnd.NextDouble() < attr.Probability;
                        Printer.Write(PrintType.Info, 
                            String.Format("{0} {1} {2}", humans[i].Representation, 
                                (liked[i] ? "like" : "not like"),
                                humans[(i + 1) % humans.Count].Representation));
                        break;
                    }
                }
            }
            if (liked.Any(x => !x)) { return null; }

            Type t = GetTypeByName(childType);

            var constructor = t.GetConstructors(System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance)[0];
            string name = GenerateChildName(second);
            var result = (IHasName)constructor.Invoke(new object[] { name });
            SetPatronymicIfExists(result, first, second);
            return result;
        }

        private string GenerateChildName(Human human)
        {
            var method = human.GetType().GetMethods(System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance).FirstOrDefault(x => 
                x.ReturnType == typeof(string) && x.GetParameters().Length == 0);
            if (method == null)
            {
                throw new NotImplementedException("Name generation method not found");
            }
            return (string)method.Invoke(human, null);    
        }

        private Type GetTypeByName(string name)
        {
            return Type.GetType(String.Format("{0}.{1}, {2}", MyNamespace, name, MyAssembly));
        }

        private void SetPatronymicIfExists(IHasName target, Human first, Human second)
        {
            var patronymicProperty = target.GetType().GetProperties(System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance).FirstOrDefault(x => x.Name == PatronymicPropertyName);
            if (patronymicProperty != null)
            {
                var s = NamesHelper.GeneratePatronimyc((first.Gender == Gender.Male ? first : second).Name, Gender.Female);
                patronymicProperty.SetValue(target, s);
            }
        }
    }
}
