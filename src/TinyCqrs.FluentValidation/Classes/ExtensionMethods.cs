using System.Threading.Tasks;
using FluentValidation.Results;
using TinyCqrs.Classes;
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
            if (current.Success && next != null)
            {
                var nextResult = next.Execute(cmd);
                return nextResult;
            }

            return current;
        }

        public static async Task<ValidationCmdResult> ThisOrNext<TCmd>(this Task<ValidationResult> validationTask, TCmd cmd, ICmdHandlerAsync<TCmd, ValidationCmdResult> next)
        {
            var validationResult = await validationTask;
            var current = new ValidationCmdResult(validationResult);
            
            // Next should only be allowable as null for the sake of Unit testing,
            // allowing the validator decorator to be independently tested.
            if (current.Success && next != null)
            {
                var nextResult = await next.Execute(cmd);
                return nextResult;
            }

            return current;
        }
    }
}