using TinyCqrs.Classes;

namespace TinyCqrs.Interfaces
{
    public interface ICmdHandler<in TCmd>
    {
        CmdResult Execute(TCmd cmd);
    }
    
    public interface ICmdHandler<in TCmd, out TCmdResult>
        where TCmdResult : ICmdResult
    {
        TCmdResult Execute(TCmd cmd);
    }
}