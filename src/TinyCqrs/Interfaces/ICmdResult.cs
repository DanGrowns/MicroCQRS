using System.Collections.Generic;
using TinyCqrs.Classes;
using TinyCqrs.Enums;

namespace TinyCqrs.Interfaces
{
    public interface ICmdResult
    {
        string SourceName { get; }
        bool Success { get; }
        List<CmdIssue> Issues { get; }
        void AddIssue(string issueMessage, IssueType type = IssueType.Error);
    }
}