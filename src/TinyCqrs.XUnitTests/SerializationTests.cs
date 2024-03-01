using System.Text.Json;
using FluentAssertions;
using TinyCqrs.Classes;
using TinyCqrs.Enums;
using Xunit;

namespace TinyCqrs.XUnitTests
{
    public class SerializationTests
    {
        [Fact]
        public void CmdIssue_Serialization_Ok()
        {
            var result = new CmdIssue("The Message");

            var serialized = JsonSerializer.Serialize(result);
            var deserialized = JsonSerializer.Deserialize<CmdIssue>(serialized);

            deserialized.Should().NotBeNull();
            deserialized.Type.Should().Be(IssueType.Error);
            deserialized.Message.Should().Be("The Message");
        }
        
        [Fact]
        public void CmdResult_Serialization_Ok()
        {
            var result = new CmdResult<object>("Test result");
            result.AddIssue("Test message");
            
            var serialized = JsonSerializer.Serialize(result);
            var deserialized = JsonSerializer.Deserialize<CmdResult<object>>(serialized);
            
            deserialized.Should().NotBeNull();
            deserialized.Type.Should().Be("Test result");
            deserialized.Success.Should().BeFalse();
            
            deserialized.Issues.Count.Should().Be(1);
            
            deserialized.Issues[0].Type.Should().Be(IssueType.Error);
            deserialized.Issues[0].Message.Should().Be("Test message");
        }
    }
}