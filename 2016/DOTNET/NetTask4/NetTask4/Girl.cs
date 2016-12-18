namespace NetTask4
{
    [Couple(Pair = "Student", Probability = 0.7, ChildType = "Girl")]
    [Couple(Pair = "Botan", Probability = 0.3, ChildType = "SmartGirl")]
    internal class Girl: Human
    {
        internal override Gender Gender { get { return Gender.Female; } }
        internal Girl(string name) : base(name) {}
    }
}
