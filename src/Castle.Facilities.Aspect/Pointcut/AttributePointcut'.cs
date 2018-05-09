using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.Core;
using Castle.DynamicProxy;
using Castle.MicroKernel;

namespace Castle.Facilities.Aspect
{
    /// <summary>
    /// a predefined pointcut declaration to intercept methods decorated with of an attribute
    /// </summary>
    /// <typeparam name="T">The attribute type to check</typeparam>
    /// <typeparam name="TInterceptor">The actual implementation of the interceptor to apply</typeparam>
    public class AttributePointcut<T, TInterceptor> : BasePointcut<TInterceptor>
        where T : Attribute
        where TInterceptor : class, IInterceptor
    {
        public AttributePointcut(IKernel kernel)
            : base(kernel)
        {
        }

        public override IEnumerable<MethodInfo> CanIntercept(ComponentModel model)
        {
            var implementation = model.Implementation;

            return implementation.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                   .Where(methodInfo => methodInfo.GetCustomAttributes(typeof(T), true).Any());
        }

        public override bool CanIntercept(Type type, MethodInfo method)
        {
            if (type.IsBasedOn(method.DeclaringType))
                return TypeExtensions.GetCustomAttributes<T>(type, method) != null;

            return true;
        }
    }
}
