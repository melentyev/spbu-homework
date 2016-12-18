using System;

namespace NetTask4
{
    internal abstract class Human: IHasName
    {
        internal string RandomChildName() { return NamesHelper.RandomName(Gender.Female); }

        internal string Patronymic { get; set; }
        internal abstract Gender Gender { get; }

        private string name;

        public string Name { get { return name; } }
        public string Representation
        {
            get
            {
                return GetType().Name + "(" + Name + (String.IsNullOrEmpty(Patronymic) ? "" : " " + Patronymic) + ")";
            }
        }

        internal Human(string name)
        {
            this.name = name;
        }
    }
}
