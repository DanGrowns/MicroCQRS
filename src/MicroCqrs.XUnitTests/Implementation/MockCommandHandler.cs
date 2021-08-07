using System;
using MicroCqrs.Abstract;
using MicroCqrs.Classes;
using MicroCqrs.Interfaces;

namespace MicroCqrs.XUnitTests.Implementation
{
    public sealed class MockCoreCommand2
    {
        public bool ThrowError { get; set; }
    }
    
    public class MockCommandHandler : TryCatchHandler<MockCoreCommand2>
    {
        public MockCommandHandler() 
            => CmdResult = new CmdResult("Command Handler");
        
        protected override ICmdResult CmdResult { get; }
        
        protected override void ExecuteBody(MockCoreCommand2 cmd)
        {
            if (cmd.ThrowError)
                throw new ArgumentException();
        }
    }
}