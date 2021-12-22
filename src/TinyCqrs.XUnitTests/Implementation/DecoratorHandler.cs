using System;
using System.Threading.Tasks;
using TinyCqrs.Abstract;
using TinyCqrs.Attributes;
using TinyCqrs.Classes;
using TinyCqrs.Interfaces;

namespace TinyCqrs.XUnitTests.Implementation
{
    [CqrsDecorator]
    public class DecoratorHandler : NextOnSuccessDecorator<MockCoreCommand>
    {
        public DecoratorHandler(ICmdHandler<MockCoreCommand> next) : base(next)
        {
            CmdResult = new CmdResult("Decorator handler");
            ChildHandler = next;
        }

        public ICmdHandler<MockCoreCommand> ChildHandler { get; }

        protected override void ExecuteBody(MockCoreCommand cmd)
        {
            if (cmd.ThrowError)
                throw new ArgumentException();
        }
    }

    [CqrsDecorator]
    public class DecoratorHandlerAsync : NextOnSuccessDecoratorAsync<MockCoreCommand>
    {
        public DecoratorHandlerAsync(ICmdHandlerAsync<MockCoreCommand> next) : base(next)
            => CmdResult = new CmdResult("Decorator Handler Async");

        protected override Task ExecuteBody(MockCoreCommand cmd)
        {
            if (cmd.ThrowError)
                throw new ArgumentException();
            
            return Task.CompletedTask;
        }
    }
}