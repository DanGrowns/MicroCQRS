using System.Collections.Generic;
using TinyCqrs.Classes;
using TinyCqrs.Enums;

namespace TinyCqrs.Interfaces
{
    public interface ICmdResult<TOutput>
    {
        string Type { get; }
        TOutput Output { get; set; }
        bool Success { get; }
        List<CmdIssue> Issues { get; }
        void AddIssue(string issueMessage, IssueType type = IssueType.Error);
    }
}