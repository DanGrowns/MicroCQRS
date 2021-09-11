using System;
using System.Threading.Tasks;
using TinyCqrs.Abstract;
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
    
    [CqrsDecoratedBy(typeof(DecoratorHandlerAsync))]
    public class CoreCommandHandlerAsync : TryCatchHandlerAsync<MockCoreCommand>
    {
        protected override ICmdResult CmdResult { get; } = new CmdResult("Core command handler async");
        
        protected override Task ExecuteBody(MockCoreCommand cmd)
        {
            if (cmd.ThrowError)
                throw new ArgumentException();
            
            return Task.CompletedTask;
        }
    }
}