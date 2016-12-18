using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTask4
{
    [Couple(Pair = "Girl", Probability = 0.7, ChildType = "SmartGirl")]
    [Couple(Pair = "PrettyGirl", Probability = 1.0, ChildType = "PrettyGirl")]
    [Couple(Pair = "SmartGirl", Probability = 0.8, ChildType = "Book")]
    internal sealed class Botan: Student
    {
        internal Botan(string name): base(name) {}
    }
}
