using Castle.Core.Internal;
using Castle.DynamicProxy;
using System;

namespace Castle.Facilities.Aspect
{
    public abstract class AttributeInterceptor<T> : IInterceptor where T : Attribute
    {
        public virtual void Intercept(IInvocation invocation)
        {
            T attribute = GetAttribute(invocation);
            if (attribute != null)
            {
                InnerIntercept(invocation);
            }
            else
                invocation.Proceed();
        }

        protected abstract void InnerIntercept(IInvocation invocation);

        private T GetAttribute(IInvocation invocation)
        {
            return invocation.MethodInvocationTarget.GetAttribute<T>();
        }
    }
}