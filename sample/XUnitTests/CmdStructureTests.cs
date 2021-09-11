using Domain.Models;
using Domain.Models.Read;
using FluentAssertions;
using Servicing.Features.Authors;
using Servicing.Features.Blogs;
using Servicing.Features.Posts;
using Servicing.Interfaces;
using TinyCqrs.Classes;
using TinyCqrs.Interfaces;
using Xunit;

namespace XUnitTests
{
    public class CmdStructureTests
    {
        private CqrsConfigurationTester Tester { get; }

        public CmdStructureTests() 
            => Tester = new CqrsConfigurationTester(typeof(IProjectDbContext));

        [Fact]
        public void HasNoDuplicateConfigurations_Ok()
            => Tester.HasDuplicateConfigurations().Should().BeFalse();
        
        [Fact]
        public void Author_Pipeline_Ok()
        {
            Tester.HandlerPipelineEquals(typeof(ICmdHandlerAsync<Author>), new[]
            {
                typeof(ValidateAuthor),
                typeof(SaveAuthor)
            });
        }
        
        [Fact]
        public void Blog_Pipeline_Ok()
        {
            Tester.HandlerPipelineEquals(typeof(ICmdHandlerAsync<Blog>), new[]
            {
                typeof(ValidateBlog),
                typeof(SaveBlog)
            });
        }
        
        [Fact]
        public void Post_Pipeline_Ok()
        {
            Tester.HandlerPipelineEquals(typeof(ICmdHandlerAsync<PostDisplay>), new[]
            {
                typeof(ValidatePost),
                typeof(SavePost)
            });
        }
        
        [Fact]
        public void DeleteAuthor_Single_Ok()
        {
            Tester.HandlerPipelineEquals(typeof(ICmdHandlerAsync<DeleteAuthorCmd>), new[]
            {
                typeof(DeleteAuthor)
            });
        }
        
        [Fact]
        public void DeleteBlog_Single_Ok()
        {
            Tester.HandlerPipelineEquals(typeof(ICmdHandlerAsync<DeleteBlogCmd>), new[]
            {
                typeof(DeleteBlog)
            });
        }
        
        [Fact]
        public void DeletePost_Single_Ok()
        {
            Tester.HandlerPipelineEquals(typeof(ICmdHandlerAsync<DeletePostCmd>), new[]
            {
                typeof(DeletePost)
            });
        }
    }
}