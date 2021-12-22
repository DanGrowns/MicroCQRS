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
    public class CustomResult : ICmdResult
    {
        public string SourceName { get; }
        public bool Success { get; }
        public List<CmdIssue> Issues { get; }
        public void AddIssue(string issueMessage, IssueType type = IssueType.Error) { }
    }

    [ExcludeFromCodeCoverage]
    public class CmdHandler1 : ICmdHandler<Cmd>
    {
        public CmdResult Execute(Cmd cmd)
        {
            return new CmdResult();
        }
    }

    [ExcludeFromCodeCoverage]
    public class CmdHandler2 : ICmdHandler<Cmd, CustomResult>
    {
        public CustomResult Execute(Cmd cmd)
        {
            return new CustomResult();
        }
    }

    [ExcludeFromCodeCoverage]
    public class CmdHandlerAsync1 : ICmdHandlerAsync<Cmd>
    {
        public Task<CmdResult> Execute(Cmd cmd)
        {
            return Task.FromResult(new CmdResult());
        }
    }

    [ExcludeFromCodeCoverage]
    public class CmdHandlerAsync2 : ICmdHandlerAsync<Cmd, CustomResult>
    {
        public Task<CustomResult> Execute(Cmd cmd)
        {
            return Task.FromResult(new CustomResult());
        }
    }
}