using System;

namespace Castle.Facilities.Aspect.SampleApp
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class LogAttribute : Attribute
    {

    }
}