namespace MicroCqrs.Interfaces
{
    // The type parameter is used for design time checking against the QueryHandler,
    // to ensure they are both returning the same type.
    
    // ReSharper disable once UnusedTypeParameter
    public interface IQuery<TResult> { }
}