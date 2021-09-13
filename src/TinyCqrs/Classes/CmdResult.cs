using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json.Serialization;
using TinyCqrs.Enums;
using TinyCqrs.Interfaces;

namespace TinyCqrs.Classes
{
    public class CmdResult : ICmdResult
    {
        [ExcludeFromCodeCoverage]
        public CmdResult() => Issues = new List<CmdIssue>();
        public CmdResult(string sourceName) : this() => SourceName = sourceName;
        
        [JsonConstructor]
        public CmdResult(string sourceName, List<CmdIssue> issues) : this()
        {
            SourceName = sourceName;
            Issues = issues;
        }
        
        public string SourceName { get; }
        public List<CmdIssue> Issues { get; }
        
        public bool Success 
            => Issues.Any(x => x.Type == IssueType.Error) == false;
        
        public void AddIssue(string issueMessage, IssueType type = IssueType.Error) 
            => Issues.Add(new CmdIssue(SourceName, issueMessage, type));
    }
}