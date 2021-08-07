using System.Threading.Tasks;

namespace MicroCqrs.Interfaces
{
    public interface ICmdHandlerAsync<TCommand>
    {
        Task<ICmdResult> Execute(TCommand cmd);
    }
}