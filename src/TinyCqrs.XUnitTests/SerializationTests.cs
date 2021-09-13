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
            var result = new CmdIssue("Source", "The Message");

            var serialized = JsonSerializer.Serialize(result);
            var deserialized = JsonSerializer.Deserialize<CmdIssue>(serialized);

            deserialized.Should().NotBeNull();
            deserialized.SourceName.Should().Be("Source");
            deserialized.Type.Should().Be(IssueType.Error);
            deserialized.Message.Should().Be("The Message");
        }
        
        [Fact]
        public void CmdResult_Serialization_Ok()
        {
            var result = new CmdResult("Test result");
            result.AddIssue("Test message");
            
            var serialized = JsonSerializer.Serialize(result);
            var deserialized = JsonSerializer.Deserialize<CmdResult>(serialized);
            
            deserialized.Should().NotBeNull();
            deserialized.SourceName.Should().Be("Test result");
            deserialized.Success.Should().BeFalse();
            
            deserialized.Issues.Count.Should().Be(1);
            
            deserialized.Issues[0].Type.Should().Be(IssueType.Error);
            deserialized.Issues[0].Message.Should().Be("Test message");
            deserialized.Issues[0].SourceName.Should().Be("Test result");
        }
    }
}