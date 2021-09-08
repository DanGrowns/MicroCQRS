using System.Threading.Tasks;

namespace TinyCqrs.Interfaces
{
    /// <summary>
    /// A query handler that takes no parameters and returns a result
    /// </summary>
    public interface IQueryHandler<TResult>
    {
        TResult Get();
    }
    
    /// <summary>
    /// A query handler that takes a parameter and returns a result. Type checks the query against the result.
    /// </summary>
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        TResult Get(TQuery query);
    }
    
    /// <summary>
    /// An asynchronous query handler that takes no parameters and returns a result
    /// </summary>
    public interface IQueryHandlerAsync<TResult>
    {
        Task<TResult> Get();
    }

    /// <summary>
    /// An asynchronous query handler that takes a parameter and returns a result. Type checks the query against the result.
    /// </summary>
    public interface IQueryHandlerAsync<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<TResult> Get(TQuery query);
    }
}