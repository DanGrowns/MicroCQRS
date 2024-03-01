using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using TinyCqrs.Classes;
using TinyCqrs.Enums;
using TinyCqrs.Interfaces;

namespace TinyCqrs.XUnitTests.Implementation
{
    [ExcludeFromCodeCoverage]
    public class Cmd {}

    [ExcludeFromCodeCoverage]
    public class CustomResult : ICmdResult<object>
    {
        public string Type { get; }
        public object Output { get; set; }
        public bool Success { get; }
        public List<CmdIssue> Issues { get; }
        public void AddIssue(string issueMessage, IssueType type = IssueType.Error) { }
    }

    [ExcludeFromCodeCoverage]
    public class CmdHandler1 : ICmdHandler<Cmd>
    {
        public ICmdResult<object> Execute(Cmd cmd)
        {
            return new CmdResult<object>();
        }
    }

    [ExcludeFromCodeCoverage]
    public class CmdHandler2 : ICmdHandler<Cmd, CustomResult>
    {
        public ICmdResult<CustomResult> Execute(Cmd cmd)
        {
            return new CmdResult<CustomResult>();
        }
    }

    [ExcludeFromCodeCoverage]
    public class CmdHandlerAsync1 : ICmdHandlerAsync<Cmd>
    {
        public Task<ICmdResult<object>> Execute(Cmd cmd)
        {
            return Task.FromResult(
                (ICmdResult<object>) new CmdResult<object>());
        }
    }

    [ExcludeFromCodeCoverage]
    public class CmdHandlerAsync2 : ICmdHandlerAsync<Cmd, CustomResult>
    {
        public Task<ICmdResult<CustomResult>> Execute(Cmd cmd)
        {
            return Task.FromResult(
                (ICmdResult<CustomResult>) new CmdResult<CustomResult>());
        }
    }
}