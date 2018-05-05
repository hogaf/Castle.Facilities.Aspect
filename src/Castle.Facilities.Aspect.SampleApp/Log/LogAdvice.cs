using System;
using Castle.DynamicProxy;

namespace Castle.Facilities.Aspect.SampleApp
{
    public class LogAdvice : AttributeInterceptor<LogAttribute>
    {
        protected override void InnerIntercept(IInvocation invocation)
        {
            Console.WriteLine("DumpInterceptorCalled on method " + invocation.Method.Name);
            invocation.Proceed();
            if (invocation.Method.ReturnType == typeof(int))
            {
                invocation.ReturnValue = (int)invocation.ReturnValue + 1;
            }
            Console.WriteLine("DumpInterceptor returnvalue is " + (invocation.ReturnValue ?? "NULL"));
        }
    }
}