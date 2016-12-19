using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTask4
{
    internal sealed class Book: IHasName
    {
        private string name;
        public string Name { get { return name; } }

        public string Representation { get { return String.Format("Book({0})", Name); } }

        internal Book(string name)
        {
            this.name = name;
        }
    }
}
