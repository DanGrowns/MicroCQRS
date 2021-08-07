using System;

namespace TinyCqrs.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class CqrsIgnore : Attribute
    {
    }
}