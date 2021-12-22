using System.Threading.Tasks;
using TinyCqrs.Classes;

namespace TinyCqrs.Interfaces
{
    public interface ICmdHandlerAsync<in TCmd>
    {
        Task<CmdResult> Execute(TCmd cmd);
    }
    
    public interface ICmdHandlerAsync<in TCmd, TCmdResult>
        where TCmdResult : ICmdResult
    {
        Task<TCmdResult> Execute(TCmd cmd);
    }
}