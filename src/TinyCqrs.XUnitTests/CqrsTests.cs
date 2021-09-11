using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation.Results;
using TinyCqrs.Classes;
using TinyCqrs.FluentValidation.Classes;
using TinyCqrs.Interfaces;
using TinyCqrs.XUnitTests.Implementation;
using Microsoft.Extensions.DependencyInjection;
using TinyCqrs.Enums;
using Xunit;

namespace TinyCqrs.XUnitTests
{
    public class CqrsTests
    {
        private static int CountHandlers => 7;
        
        [Fact]
        public void CmdResult_AddError_Ok()
        {
            const string source = "Source";
            const string message = "New error message";
            
            var basic = new CmdResult(source);
            basic.AddIssue(message);
            basic.AddIssue("New warning message", IssueType.Warning);

            basic.SourceName.Should().Be(source);
            basic.Issues.Count.Should().Be(2);
            basic.Issues[0].SourceName.Should().Be(source);
            basic.Issues[0].Message.Should().Be(message);

            basic.Success.Should().BeFalse();
        }
        
        [Theory]
        [InlineData(true, false)]
        [InlineData(false, false)]
        public void ValidationCmdResult_AddError_Ok(bool ignoreWarnings, bool isSuccessful)
        {
            const string source = "Validation";
            const string validationError = "An error occured";
            
            var vcr = new ValidationCmdResult(new ValidationResult {Errors = { new ValidationFailure("Property", validationError)}});
            vcr.AddIssue("New error message");
            vcr.AddIssue("Warning message", IssueType.Warning);

            vcr.SourceName.Should().Be(source);
            vcr.Issues.Count.Should().Be(2);
            vcr.Issues.Count(x => x.Type == IssueType.Warning).Should().Be(1);
            
            vcr.Issues[0].SourceName.Should().Be(source);
            vcr.Issues[0].Message.Should().Be(validationError);

            vcr.Success.Should().Be(isSuccessful);
        }

        [Fact]
        public void ConfigureCqrsObjects_ByTypeAssembly_Ok()
        {
            var services = new MockServiceCollection();
            services.ConfigureCqrsObjects(typeof(CoreCommandHandler));

            services.AddedItems.Count.Should().Be(CountHandlers, "decorators do not get included in the added items.");
        }
        
        [Fact]
        public void ConfigureCqrsObjects_ByTypeParameter_Ok()
        {
            var services = new MockServiceCollection();
            services.ConfigureCqrsObjects<CoreCommandHandler>();

            services.AddedItems.Count.Should().Be(CountHandlers, "decorators do not get included in the added items.");
        }
        
        [Fact]
        public void ConfigureCqrsObjects_ByExecutingAssembly_Ok()
        {
            var services = new MockServiceCollection();
            services.ConfigureCqrsObjects();

            services.AddedItems.Count.Should().Be(0, "RhLibs.Cqrs has no implementation types."); 
        }
        
        [Fact]
        public void ConfigureCqrsObjects_BySpecifiedAssembly_Ok()
        {
            var services = new MockServiceCollection();
            services.ConfigureCqrsObjects(Assembly.GetExecutingAssembly());

            services.AddedItems.Count.Should().Be(CountHandlers, "decorators do not get included in the added items.");
        }

        [Fact]
        public void TestForDuplicateConfigurations()
        {
            var services = new CqrsConfigurationTester(typeof(CoreCommandHandler));
            services.HasDuplicateConfigurations().Should().BeFalse();
        }

        [Theory]
        [InlineData(false, true)]
        [InlineData(true, false)]
        public void CmdHandler_TryCatch_Ok(bool throwError, bool isSuccessful)
        {
            var cmd = new MockCoreCommand2 {ThrowError = throwError};
            var handler = new MockCommandHandler();

            var result = handler.Execute(cmd);
            result.Success.Should().Be(isSuccessful);
        }
        
        [Theory]
        [InlineData(false, true)]
        [InlineData(true, false)]
        public async Task CmdHandlerAsync_TryCatch_Ok(bool throwError, bool isSuccessful)
        {
            var cmd = new MockCoreCommand(throwError);
            var handler = new CoreCommandHandlerAsync();

            var result = await handler.Execute(cmd);
            result.Success.Should().Be(isSuccessful);
        }

        [Fact]
        public void HandlerRegistrations_Ok()
        {
            var sc = new ServiceCollection();
            sc.ConfigureCqrsObjects(typeof(CoreCommandHandler));
            
            var provider = sc.BuildServiceProvider();

            var decoratedService = provider.GetService<ICmdHandler<MockCoreCommand>>();
            decoratedService.Should().BeOfType<DecoratorHandler>();

            var parent = (DecoratorHandler) decoratedService;

            if (parent == null)
                throw new Exception("Parent should not be null");

            parent.ChildHandler.Should().BeOfType<CoreCommandHandler>();

            var result = parent.Execute(new MockCoreCommand(false));
            result.SourceName.Should().Be("Core command handler");
        }

        [Theory]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData("Some data", true)]
        public void ThisOrNext_Ok(string testData, bool isSuccessful)
        {
            var data = new ThisOrNextData {Test = testData};
            var handler = new ThisOrNextHandler();
            var decorator = new ThisOrNextValidator(handler);

            var result = decorator.Execute(data);
            result.Success.Should().Be(isSuccessful);
        }
        
        [Theory]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData("Some data", true)]
        public async Task ThisOrNextAsync_Ok(string testData, bool isSuccessful)
        {
            var data = new ThisOrNextData {Test = testData};
            var handler = new ThisOrNextHandlerAsync();
            var decorator = new ThisOrNextValidatorAsync(handler);

            var result = await decorator.Execute(data);
            result.Success.Should().Be(isSuccessful);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public async Task NextOnSuccessDecoratorAsync_Ok(bool throwError, bool isSuccessful)
        {
            var data = new MockCoreCommand(throwError);
            var handler = new CoreCommandHandlerAsync();
            var decorator = new DecoratorHandlerAsync(handler);

            var result = await decorator.Execute(data);
            result.Success.Should().Be(isSuccessful);
        }
        
        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void NextOnSuccessDecorator_Ok(bool throwError, bool isSuccessful)
        {
            var data = new MockCoreCommand(throwError);
            var handler = new CoreCommandHandler();
            var decorator = new DecoratorHandler(handler);

            var result = decorator.Execute(data);
            result.Success.Should().Be(isSuccessful);
        }

        [Fact]
        public void GetPipeline_Ok()
        {
            var tester = new CqrsConfigurationTester(typeof(DecoratorHandlerAsync));
            var pipeline = tester.GetCqrsPipeline<ICmdHandlerAsync<MockCoreCommand>>();
            pipeline.Count.Should().Be(2);
        }

        [Fact]
        public void ConfigurationTester_ConstructByAssy_Ok()
        {
            var tester = new CqrsConfigurationTester(Assembly.GetExecutingAssembly());
            tester.IsAssemblyNull().Should().BeFalse();
        }
    }
}