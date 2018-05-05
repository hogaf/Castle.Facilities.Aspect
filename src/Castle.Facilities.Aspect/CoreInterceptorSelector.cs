using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace Castle.Facilities.Aspect
{
    public class CoreInterceptorSelector : IInterceptorSelector
    {
        public IPointcut Pointcut { get; private set; }
        public IInterceptorSelector ParentInterceptorSelector { get; private set; }

        public CoreInterceptorSelector(IPointcut pointcut)
        {
            Pointcut = pointcut;
        }

        public CoreInterceptorSelector(IInterceptorSelector parentInterceptorSelector, IPointcut pointcut)
        {
            ParentInterceptorSelector = parentInterceptorSelector;
            Pointcut = pointcut;
        }

        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            if (ParentInterceptorSelector != null)
                interceptors = ParentInterceptorSelector.SelectInterceptors(type, method, interceptors);

            interceptors = Pointcut.SelectInterceptors(type, method, interceptors);

            return interceptors;
        }
    }
}