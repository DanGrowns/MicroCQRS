using System.Text.Json.Serialization;
using TinyCqrs.Enums;

namespace TinyCqrs.Classes
{
    public class CmdIssue
    {
        [JsonConstructor]
        public CmdIssue(string message, IssueType type = IssueType.Error)
        {
            Message = message;
            Type = type;
        }
        
        public string Message { get; }
        public IssueType Type { get; }
    }
}