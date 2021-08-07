namespace TinyCqrs.Classes
{
    public class CmdIssue
    {
        public CmdIssue(string sourceName, string errorMessage)
        {
            SourceName = sourceName;
            ErrorMessage = errorMessage;
        }
        
        public string SourceName { get; }
        public string ErrorMessage { get; }
    }
}