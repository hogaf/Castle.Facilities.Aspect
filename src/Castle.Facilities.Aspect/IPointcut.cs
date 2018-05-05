using Castle.Core;
using Castle.DynamicProxy;

namespace Castle.Facilities.Aspect
{
    public interface IPointcut : IInterceptorSelector
    {
        void ApplyInterceptor(ComponentModel model);
    }
}
