using TinyCqrs.Classes;
using TinyCqrs.Interfaces;

namespace TinyCqrs.XUnitTests.Implementation
{
    public class Cmd1 {}
    public class Cmd2 {}
    
    public class MultipleContracts : ICmdHandler<Cmd1>, ICmdHandler<Cmd2>
    {
        public CmdResult Execute(Cmd1 cmd)
        {
            throw new System.NotImplementedException();
        }

        public CmdResult Execute(Cmd2 cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}