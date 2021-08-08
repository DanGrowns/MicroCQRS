using System.Threading.Tasks;
using Domain.Models;
using FluentValidation;
using TinyCqrs.Attributes;
using TinyCqrs.FluentValidation.Classes;
using TinyCqrs.Interfaces;

namespace Servicing.Features.Authors
{
    [CqrsDecorator]
    public class ValidateAuthor : AbstractValidator<Author>, ICmdHandlerAsync<Author>
    {
        private ICmdHandlerAsync<Author> Next { get; }

        public ValidateAuthor(ICmdHandlerAsync<Author> next)
        {
            Next = next;

            RuleFor(x => x.Forename).NotEmpty().WithMessage("Forename required");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname required");
        }

        public async Task<ICmdResult> Execute(Author cmd)
            => await ValidateAsync(cmd).ThisOrNext(cmd, Next);
    }
}