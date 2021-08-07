using System.Threading.Tasks;
using FluentValidation.Results;
using TinyCqrs.Interfaces;

namespace TinyCqrs.FluentValidation.Classes
{
    public static class ExtensionMethods
    {
        public static ICmdResult ThisOrNext<TCmd>(this ValidationResult validationResult, TCmd cmd, ICmdHandler<TCmd> next)
        {
            var current = new ValidationCmdResult(validationResult);
            
            // Next should only be allowable as null for the sake of Unit testing,
            // allowing the validator decorator to be independently tested.
            if (current.IsSuccessful() && next != null)
            {
                var nextResult = next.Execute(cmd);
                return nextResult;
            }

            return current;
        }
        
        public static async Task<ICmdResult> ThisOrNext<TCmd>(this Task<ValidationResult> validationTask, TCmd cmd, ICmdHandlerAsync<TCmd> next)
        {
            var validationResult = await validationTask;
            var current = new ValidationCmdResult(validationResult);
            
            // Next should only be allowable as null for the sake of Unit testing,
            // allowing the validator decorator to be independently tested.
            if (current.IsSuccessful() && next != null)
            {
                var nextResult = await next.Execute(cmd);
                return nextResult;
            }

            return current;
        }
    }
}