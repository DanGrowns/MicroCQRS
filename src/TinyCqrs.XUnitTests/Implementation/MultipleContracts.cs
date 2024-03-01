using TinyCqrs.Classes;
using TinyCqrs.Interfaces;

namespace TinyCqrs.XUnitTests.Implementation
{
    public class Cmd1 {}
    public class Cmd2 {}
    
    public class MultipleContracts : ICmdHandler<Cmd1>, ICmdHandler<Cmd2>
    {
        public ICmdResult<object> Execute(Cmd1 cmd)
        {
            throw new System.NotImplementedException();
        }

        public ICmdResult<object> Execute(Cmd2 cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}