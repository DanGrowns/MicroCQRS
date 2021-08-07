using System;
using System.Threading.Tasks;
using MicroCqrs.Abstract;
using MicroCqrs.Attributes;
using MicroCqrs.Classes;
using MicroCqrs.Interfaces;

namespace MicroCqrs.XUnitTests.Implementation
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