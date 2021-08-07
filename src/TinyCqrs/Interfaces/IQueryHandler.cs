using System.Threading.Tasks;

namespace TinyCqrs.Interfaces
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        TResult Get(TQuery query);
    }
    
    public interface IQueryHandlerAsync<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<TResult> Get(TQuery query);
    }
}