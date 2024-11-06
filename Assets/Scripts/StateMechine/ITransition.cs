namespace StateMechine
{
    public interface ITransition
    {
        IState To { get; }
        IPredicate Condition { get; }
    }
}