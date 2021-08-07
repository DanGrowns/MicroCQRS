using System.Threading.Tasks;
using TinyCqrs.Attributes;
using TinyCqrs.Classes;
using TinyCqrs.FluentValidation.Classes;
using TinyCqrs.Interfaces;
using FluentValidation;

namespace TinyCqrs.XUnitTests.Implementation
{
    public class ThisOrNextData
    {
        public string Test { get; set; }    
    }
    
    [CqrsDecorator]
    public class ThisOrNextValidator : AbstractValidator<ThisOrNextData>, ICmdHandler<ThisOrNextData>
    {
        private ICmdHandler<ThisOrNextData> Next { get; }
        
        public ThisOrNextValidator(ICmdHandler<ThisOrNextData> next)
        {
            Next = next;

            RuleFor(x => x.Test).NotEmpty().WithMessage("Test cannot be empty");
        }

        public ICmdResult Execute(ThisOrNextData cmd)
            => Validate(cmd).ThisOrNext(cmd, Next);
    }

    [CqrsDecorator]
    public class ThisOrNextValidatorAsync: AbstractValidator<ThisOrNextData>, ICmdHandlerAsync<ThisOrNextData>
    {
        private ICmdHandlerAsync<ThisOrNextData> Next { get; }
        
        public ThisOrNextValidatorAsync(ICmdHandlerAsync<ThisOrNextData> next)
        {
            Next = next;

            RuleFor(x => x.Test).NotEmpty().WithMessage("Test cannot be empty");
        }

        public async Task<ICmdResult> Execute(ThisOrNextData cmd)
            => await ValidateAsync(cmd).ThisOrNext(cmd, Next);
    }

    [CqrsDecoratedBy(typeof(ThisOrNextValidator))]
    public class ThisOrNextHandler : ICmdHandler<ThisOrNextData>
    {
        public ICmdResult Execute(ThisOrNextData cmd)
        {
            return new CmdResult("");
        }
    }
    
    [CqrsDecoratedBy(typeof(ThisOrNextValidatorAsync))]
    public class ThisOrNextHandlerAsync : ICmdHandlerAsync<ThisOrNextData>
    {
        public async Task<ICmdResult> Execute(ThisOrNextData cmd)
        {
            return new CmdResult("");
        }
    }
}