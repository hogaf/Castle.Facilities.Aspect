using System;

namespace Castle.Facilities.Aspect.Tests
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class PlusOneAttribute : Attribute
    {

    }
}