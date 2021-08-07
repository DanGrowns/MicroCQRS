using TinyCqrs.Attributes;
using TinyCqrs.Classes;
using TinyCqrs.Interfaces;
using TinyCqrs.XUnitTests.Attributes;

namespace TinyCqrs.XUnitTests.Implementation
{
    public sealed class MockCoreCommand
    {
        public MockCoreCommand(bool throwError)
            => ThrowError = throwError;
        
        public bool ThrowError { get; }
    }
    
    [Dummy]
    [CqrsDecoratedBy(typeof(DecoratorHandler))]
    public class CoreCommandHandler : ICmdHandler<MockCoreCommand>
    {
        public ICmdResult Execute(MockCoreCommand cmd)
        {
            return new CmdResult("Core command handler");
        }
    }
}