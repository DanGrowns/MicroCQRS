using System;

namespace MicroCqrs.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class CqrsDecorator : Attribute
    {
    }
}