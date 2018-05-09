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
    /// a predefined pointcut declaration to intercept based on inheritance or implemntation of interfaces/abstract classes
    /// </summary>
    /// <typeparam name="T">The type that to check for any other assignable types</typeparam>
    /// <typeparam name="TInterceptor">The actual implementation of the interceptor to apply</typeparam>
    public class InheritancePointcut<T, TInterceptor> : BasePointcut<TInterceptor>
        where T : class 
        where TInterceptor : class, IInterceptor
    {
        public InheritancePointcut(IKernel kernel)
            : base(kernel)
        {
        }

        public override IEnumerable<MethodInfo> CanIntercept(ComponentModel model)
        {
            var implementation = model.Implementation;

            if (typeof(T).IsAssignableFrom(implementation))
                return implementation.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(methodInfo => methodInfo.IsVirtual);

            return Enumerable.Empty<MethodInfo>();
        }

        public override bool CanIntercept(Type type, MethodInfo method)
        {
            if (type.IsBasedOn(method.DeclaringType))
                return typeof(T).IsAssignableFrom(type) && method.IsVirtual;

            return true;
        }
    }
}
