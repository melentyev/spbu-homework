using System;

namespace NetTask4
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    internal sealed class CoupleAttribute: Attribute
    {
        public string Pair { get; set; }
        public double Probability { get; set; }
        public string ChildType { get; set; }
    }
}
