namespace NetTask4
{
    internal interface IGod
    {
        Human CreateHuman();
        IHasName Couple(Human first, Human second);
    }
}