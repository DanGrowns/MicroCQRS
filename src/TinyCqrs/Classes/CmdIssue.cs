using System.Text.Json.Serialization;
using TinyCqrs.Enums;

namespace TinyCqrs.Classes
{
    public class CmdIssue
    {
        [JsonConstructor]
        public CmdIssue(string sourceName, string message, IssueType type = IssueType.Error)
        {
            SourceName = sourceName;
            Message = message;
            Type = type;
        }
        
        public string SourceName { get; }
        public string Message { get; }
        public IssueType Type { get; }
    }
}