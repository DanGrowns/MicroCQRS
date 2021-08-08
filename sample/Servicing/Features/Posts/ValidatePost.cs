using System.Threading.Tasks;
using Domain.Models.Read;
using FluentValidation;
using TinyCqrs.Attributes;
using TinyCqrs.FluentValidation.Classes;
using TinyCqrs.Interfaces;

namespace Servicing.Features.Posts
{
    [CqrsDecorator]
    public class ValidatePost : AbstractValidator<PostDisplay>, ICmdHandlerAsync<PostDisplay>
    {
        private ICmdHandlerAsync<PostDisplay> Next { get; }

        public ValidatePost(ICmdHandlerAsync<PostDisplay> next)
        {
            Next = next;

            RuleFor(x => x.Post.Title).NotEmpty().WithMessage("You must have a title");
            RuleFor(x => x.Post.Content).NotEmpty().WithMessage("You must have some content");
            RuleFor(x => x.Blog.Id).GreaterThan(0).WithMessage("You must apply your post to a blog");
            RuleFor(x => x.PostAuthors.Count).GreaterThan(0).WithMessage("You must have at least one author");
        }

        public async Task<ICmdResult> Execute(PostDisplay cmd)
            => await ValidateAsync(cmd).ThisOrNext(cmd, Next);
    }
}