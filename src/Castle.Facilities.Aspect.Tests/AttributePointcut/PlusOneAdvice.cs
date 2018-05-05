using Castle.DynamicProxy;

namespace Castle.Facilities.Aspect.Tests
{
    public class PlusOneAdvice : AttributeInterceptor<PlusOneAttribute>
    {
        protected override void InnerIntercept(IInvocation invocation)
        {
            invocation.Proceed();
            if (invocation.Method.ReturnType == typeof(int))
            {
                invocation.ReturnValue = (int)invocation.ReturnValue + 1;
            }
        }
    }
}