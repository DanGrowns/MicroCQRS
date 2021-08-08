using System.Threading.Tasks;
using Domain.Models;
using FluentValidation;
using TinyCqrs.Attributes;
using TinyCqrs.FluentValidation.Classes;
using TinyCqrs.Interfaces;

namespace Servicing.Features.Blogs
{
    [CqrsDecorator]
    public class ValidateBlog : AbstractValidator<Blog>, ICmdHandlerAsync<Blog>
    {
        private ICmdHandlerAsync<Blog> Next { get; }

        public ValidateBlog(ICmdHandlerAsync<Blog> next)
        {
            Next = next;

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name required");
        }

        public async Task<ICmdResult> Execute(Blog cmd)
            => await ValidateAsync(cmd).ThisOrNext(cmd, Next);
    }
}