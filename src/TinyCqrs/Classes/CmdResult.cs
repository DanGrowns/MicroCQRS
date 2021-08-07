using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TinyCqrs.Interfaces;

namespace TinyCqrs.Classes
{
    public class CmdResult : ICmdResult
    {
        [ExcludeFromCodeCoverage]
        public CmdResult() : this("") { }
        
        public CmdResult(string sourceName)
        {
            SourceName = sourceName;
            Errors = new List<CmdIssue>();
            Warnings = new List<CmdIssue>();
        }

        public string SourceName { get; }
        public List<CmdIssue> Errors { get; }
        public List<CmdIssue> Warnings { get; }

        public void AddError(string errorMessage)
            => Errors.Add(new CmdIssue(SourceName, errorMessage));
        
        public void AddWarning(string errorMessage)
            => Warnings.Add(new CmdIssue(SourceName, errorMessage));
        
        public bool IsSuccessful(bool ignoreWarnings = true)
        {
            if (ignoreWarnings)
                return Errors.Count == 0;

            return Errors.Count == 0 && Warnings.Count == 0;
        }
    }
}