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
            var pipeline = Tester.GetCqrsPipeline<ICmdHandlerAsync<Author>>();
            
            pipeline.Count.Should().Be(2);
            pipeline[0].Should().Be<ValidateAuthor>();
            pipeline[1].Should().Be<SaveAuthor>();
        }
        
        [Fact]
        public void Blog_Pipeline_Ok()
        {
            var pipeline = Tester.GetCqrsPipeline<ICmdHandlerAsync<Blog>>();
            
            pipeline.Count.Should().Be(2);
            pipeline[0].Should().Be<ValidateBlog>();
            pipeline[1].Should().Be<SaveBlog>();
        }
        
        [Fact]
        public void Post_Pipeline_Ok()
        {
            var pipeline = Tester.GetCqrsPipeline<ICmdHandlerAsync<PostDisplay>>();
            
            pipeline.Count.Should().Be(2);
            pipeline[0].Should().Be<ValidatePost>();
            pipeline[1].Should().Be<SavePost>();
        }
        
        [Fact]
        public void DeleteAuthor_Single_Ok()
        {
            var handler = Tester.GetCqrsPipeline<ICmdHandlerAsync<DeleteAuthorCmd>>();
            
            handler.Count.Should().Be(1);
            handler[0].Should().Be<DeleteAuthor>();
        }
        
        [Fact]
        public void DeleteBlog_Single_Ok()
        {
            var handler = Tester.GetCqrsPipeline<ICmdHandlerAsync<DeleteBlogCmd>>();
            
            handler.Count.Should().Be(1);
            handler[0].Should().Be<DeleteBlog>();
        }
        
        [Fact]
        public void DeletePost_Single_Ok()
        {
            var handler = Tester.GetCqrsPipeline<ICmdHandlerAsync<DeletePostCmd>>();
            
            handler.Count.Should().Be(1);
            handler[0].Should().Be<DeletePost>();
        }
    }
}