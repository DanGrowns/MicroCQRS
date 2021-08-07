using System.Threading.Tasks;

namespace TinyCqrs.Interfaces
{
    public interface ICmdHandlerAsync<TCommand>
    {
        Task<ICmdResult> Execute(TCommand cmd);
    }
}