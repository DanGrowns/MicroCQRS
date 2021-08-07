using System.Collections.Generic;
using TinyCqrs.Classes;
using TinyCqrs.Interfaces;

namespace TinyCqrs.XUnitTests.Implementation
{
    public class CustomCmdResult : ICmdResult
    {
        public CustomCmdResult()
        {
            Errors = new List<CmdIssue>();
            Warnings = new List<CmdIssue>();
        }
        
        public string SourceName { get; }
        public List<CmdIssue> Errors { get; }
        public List<CmdIssue> Warnings { get; }
        public void AddError(string errorMessage)
        {
            throw new System.NotImplementedException();
        }

        public void AddWarning(string errorMessage)
        {
            throw new System.NotImplementedException();
        }

        public bool IsSuccessful(bool ignoreWarnings = true)
        {
            throw new System.NotImplementedException();
        }
    }
}