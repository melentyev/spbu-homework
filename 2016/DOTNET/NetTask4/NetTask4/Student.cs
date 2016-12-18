using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTask4
{
    [Couple(Pair = "Girl", Probability = 0.7, ChildType = "Girl")]
    [Couple(Pair = "PrettyGirl", Probability = 1.0, ChildType = "PrettyGirl")]
    [Couple(Pair = "SmartGirl", Probability = 0.5, ChildType = "Girl")]
    internal class Student : Human
    {
        internal override Gender Gender { get { return Gender.Male; } }
        internal Student(string name): base(name) {}
    }
}
