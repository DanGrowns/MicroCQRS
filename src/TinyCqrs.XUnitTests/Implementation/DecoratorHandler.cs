using System;
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

        protected override ICmdResult CmdResult { get; }

        protected override void ExecuteBody(MockCoreCommand cmd)
        {
            if (cmd.ThrowError)
                throw new ArgumentException();
        }
    }
}