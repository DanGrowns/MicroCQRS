using System.Threading.Tasks;

namespace TinyCqrs.Interfaces
{
    public interface ICmdHandlerAsync<in TCmd>
    {
        Task<ICmdResult<object>> Execute(TCmd cmd);
    }
    
    public interface ICmdHandlerAsync<in TCmd, TOutput>
    {
        Task<ICmdResult<TOutput>> Execute(TCmd cmd);
    }
}