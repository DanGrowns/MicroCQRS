using System;
using System.Threading.Tasks;
using TinyCqrs.Abstract;
using TinyCqrs.Attributes;
using TinyCqrs.Classes;
using TinyCqrs.Interfaces;

namespace TinyCqrs.XUnitTests.Implementation
{
    [CqrsDecorator]
    public class DecoratorHandlerAsync : NextOnSuccessDecoratorAsync<MockCoreCommand>
    {
        public DecoratorHandlerAsync(ICmdHandlerAsync<MockCoreCommand> next) : base(next)
            => CmdResult = new CmdResult("Decorator Handler Async");
        
        protected override ICmdResult CmdResult { get; }

        protected override Task ExecuteBody(MockCoreCommand cmd)
        {
            if (cmd.ThrowError)
                throw new ArgumentException();
            
            return Task.CompletedTask;
        }
    }
}