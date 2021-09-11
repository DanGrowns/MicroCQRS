using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Read;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Servicing.Features.Authors;
using Servicing.Features.Blogs;
using Servicing.Features.Posts;
using Servicing.Interfaces;
using Xunit;

namespace XUnitTests
{
    public class CmdHandlerTests
    {
        [Fact]
        public async Task ValidateAuthor_TotalErrors_Ok()
        {
            var handler = new ValidateAuthor(null);
            var result = await handler.Execute(new Author());
            result.Issues.Count.Should().Be(2);
        }
        
        [Theory]
        [InlineData("Bill", "", "Surname required")]
        [InlineData("", "Gates", "Forename required")]
        public async Task ValidateAuthor_ErrorMessage_Ok(string forename, string surname, string expectedErrorMessage)
        {
            var cmd = new Author { Forename = forename, Surname = surname };
            var handler = new ValidateAuthor(null);
            var result = await handler.Execute(cmd);
            
            result.Issues.Count.Should().Be(1);
            result.Issues[0].Message.Should().Be(expectedErrorMessage);
        }
        
        [Fact]
        public async Task ValidateBlog_TotalErrors_Ok()
        {
            var handler = new ValidateBlog(null);
            var result = await handler.Execute(new Blog());
            result.Issues.Count.Should().Be(1);
            result.Issues[0].Message.Should().Be("Name required");
        }

        [Fact]
        public async Task ValidatePost_TotalErrors_Ok()
        {
            var cmd = new PostDisplay(
                new Post(), 
                new Blog(), 
                new List<Author>(), 
                new List<Author>(),
                new List<Blog>());
            
            var handler = new ValidatePost(null);
            var result = await handler.Execute(cmd);
            result.Issues.Count.Should().Be(4);
        }
        
        [Theory]
        [InlineData("Titled post", "", 1, true, "You must have some content")]
        [InlineData("", "Post with content", 1, true, "You must have a title")]
        [InlineData("Titled post", "Post with content", 1, false, "You must have at least one author")]
        [InlineData("Titled post", "Post with content", 0, true, "You must apply your post to a blog")]
        public async Task ValidatePost_ErrorMessage_Ok(string title, string content, int blogId, bool addAuthor, string expectedErrorMessage)
        {
            var authors = new List<Author>();
            
            if (addAuthor)
                authors.Add(new Author());

            var cmd = new PostDisplay(
                new Post {Title = title, Content = content}, 
                new Blog {Id = blogId},
                authors, 
                new List<Author>(),
                new List<Blog>());
                
            var handler = new ValidatePost(null);
            var result = await handler.Execute(cmd);
            
            result.Issues.Count.Should().Be(1);
            result.Issues[0].Message.Should().Be(expectedErrorMessage);
        }
    }
}