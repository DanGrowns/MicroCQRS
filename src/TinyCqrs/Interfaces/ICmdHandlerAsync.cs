using System.Threading.Tasks;

namespace TinyCqrs.Interfaces
{
    public interface ICmdHandlerAsync<in TCmd>
    {
        Task<ICmdResult> Execute(TCmd cmd);
    }
    
    public interface ICmdHandlerAsync<in TCmd, TCmdResult>
        where TCmdResult : ICmdResult
    {
        Task<TCmdResult> Execute(TCmd cmd);
    }
}