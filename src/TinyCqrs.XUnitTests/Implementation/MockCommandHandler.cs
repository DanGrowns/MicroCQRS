using System;
using TinyCqrs.Abstract;
using TinyCqrs.Classes;
using TinyCqrs.Interfaces;

namespace TinyCqrs.XUnitTests.Implementation
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