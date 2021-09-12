using System.Text.Json.Serialization;
using TinyCqrs.Enums;

namespace TinyCqrs.Classes
{
    public class CmdIssue
    {
        [JsonConstructor]
        public CmdIssue(string sourceName, string message, IssueType issueType = IssueType.Error)
        {
            SourceName = sourceName;
            Message = message;
            Type = issueType;
        }
        
        public string SourceName { get; }
        public string Message { get; }
        public IssueType Type { get; }
    }
}