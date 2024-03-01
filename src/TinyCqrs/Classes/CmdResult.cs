using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json.Serialization;
using TinyCqrs.Enums;
using TinyCqrs.Interfaces;

namespace TinyCqrs.Classes
{
    public class CmdResult<TOutput> : ICmdResult<TOutput>
    {
        [ExcludeFromCodeCoverage]
        public CmdResult() => Issues = new List<CmdIssue>();
        public CmdResult(string type) : this() => Type = type;
        public CmdResult(Enum enumValue) : this() => Type = enumValue.ToString();
        
        [JsonConstructor]
        public CmdResult(string type, List<CmdIssue> issues) : this()
        {
            Type = type;
            Issues = issues;
        }
        
        public string Type { get; }
        public TOutput Output { get; set; }
        public List<CmdIssue> Issues { get; }
        
        public bool Success 
            => Issues.Any(x => x.Type == IssueType.Error) == false;
        
        public void AddIssue(string issueMessage, IssueType type = IssueType.Error) 
            => Issues.Add(new CmdIssue(issueMessage, type));
    }
}