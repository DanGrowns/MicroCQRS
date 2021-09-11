using System.Collections.Generic;
using System.Linq;
using TinyCqrs.Classes;
using TinyCqrs.Interfaces;
using FluentValidation.Results;
using TinyCqrs.Enums;

namespace TinyCqrs.FluentValidation.Classes
{
    public class ValidationCmdResult : ICmdResult
    {
        public ValidationCmdResult(ValidationResult validationResult)
        {
            SourceName = "Validation";
            ValidationResult = validationResult;
            
            Issues = new List<CmdIssue>();
            
            if (validationResult != null)
            {
                Issues.AddRange(
                    ValidationResult.Errors
                        .Select(x => new CmdIssue(SourceName, x.ErrorMessage))
                        .ToList());
            }
        }

        public ValidationResult ValidationResult { get; }
        public string SourceName { get; }
        public List<CmdIssue> Issues { get; }
        
        public bool Success 
            => Issues.Count == 0;
        
        public void AddIssue(string issueMessage, IssueType type = IssueType.Error)
            => Issues.Add(new CmdIssue(SourceName, issueMessage, type));
    }
}