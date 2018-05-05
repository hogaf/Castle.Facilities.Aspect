using Castle.DynamicProxy;
using System;

namespace Castle.Facilities.Aspect.SampleApp
{
    public class InhertiedLogAdvice : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("Dump Inherited Interceptor Called on method " + invocation.Method.Name);
            invocation.Proceed();
        }
    }
}