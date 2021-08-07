using System.Collections.Generic;
using System.Linq;
using TinyCqrs.Classes;
using TinyCqrs.Interfaces;
using FluentValidation.Results;

namespace TinyCqrs.FluentValidation.Classes
{
    public class ValidationCmdResult : ICmdResult
    {
        public ValidationCmdResult(ValidationResult validationResult)
        {
            SourceName = "Validation";
            ValidationResult = validationResult;
            
            Errors = new List<CmdIssue>();
            Warnings = new List<CmdIssue>();

            if (validationResult != null)
            {
                Errors.AddRange(
                    ValidationResult.Errors
                        .Select(x => new CmdIssue(SourceName, x.ErrorMessage))
                        .ToList());
            }
        }

        public ValidationResult ValidationResult { get; }
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