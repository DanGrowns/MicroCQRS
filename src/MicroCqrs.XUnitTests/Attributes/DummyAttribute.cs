using System;

namespace MicroCqrs.XUnitTests.Attributes
{
    /// <summary>
    /// Used to test the HandlerRegistrar to ensure that it only picks up CqrsDecorator and CqrsDecoratedBy attributes
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class DummyAttribute : Attribute
    {
        
    }
}