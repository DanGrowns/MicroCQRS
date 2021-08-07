using System;

namespace TinyCqrs.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class CqrsDecoratedBy : Attribute
    {
        public Type ConcreteDecorator { get; }

        public CqrsDecoratedBy(Type concreteDecorator)
            => ConcreteDecorator = concreteDecorator;
    }
}