namespace MicroCqrs.Interfaces
{
    public interface ICmdHandler<TCommand>
    {
        ICmdResult Execute(TCommand cmd);
    }
}