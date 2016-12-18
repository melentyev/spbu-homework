namespace NetTask4
{
    [Couple(Pair = "Student", Probability = 0.20, ChildType = "Girl")]
    [Couple(Pair = "Botan", Probability = 0.50, ChildType = "Book")]
    internal sealed class SmartGirl : Girl
    {
        internal SmartGirl(string name): base(name) { }
    }
}
