using System.Collections.Generic;
using TinyCqrs.Classes;

namespace TinyCqrs.Interfaces
{
    public interface ICmdResult
    {
        string SourceName { get; }
        List<CmdIssue> Errors { get; }
        List<CmdIssue> Warnings { get; }

        /// <summary>
        /// Adds a single error message to the errors collection,
        /// source name of the current result will be provided.
        /// </summary>
        void AddError(string errorMessage);
        
        /// <summary>
        /// Adds a single warning message to the warnings collection,
        /// source name of the current result will be provided.
        /// </summary>
        void AddWarning(string errorMessage);

        bool IsSuccessful(bool ignoreWarnings = true);
    }
}