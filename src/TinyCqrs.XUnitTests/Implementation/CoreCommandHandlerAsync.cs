using System;
using System.Threading.Tasks;
using TinyCqrs.Abstract;
using TinyCqrs.Attributes;
using TinyCqrs.Classes;
using TinyCqrs.Interfaces;

namespace TinyCqrs.XUnitTests.Implementation
{
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