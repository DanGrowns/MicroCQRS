using TinyCqrs.Classes;

namespace TinyCqrs.Interfaces
{
    public interface ICmdHandler<in TCmd>
    {
        ICmdResult<object> Execute(TCmd cmd);
    }
    
    public interface ICmdHandler<in TCmd, TOutput>
    {
        ICmdResult<TOutput> Execute(TCmd cmd);
    }
}