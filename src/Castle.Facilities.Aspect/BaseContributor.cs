using System;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;
using Castle.MicroKernel.Proxy;

namespace Castle.Facilities.Aspect
{
    public class BaseContributor : IContributeComponentModelConstruction
    {
        public IPointcut Pointcut { get; private set; }

        public BaseContributor(IPointcut pointcut)
        {
            Pointcut = pointcut;
        }

        public void ProcessModel(IKernel kernel, ComponentModel model)
        {
            Pointcut.ApplyInterceptor(model);
            var proxyOptions = model.ObtainProxyOptions();

            if (proxyOptions.Selector == null)
                proxyOptions.Selector = new InstanceReference<CoreInterceptorSelector>(new CoreInterceptorSelector(Pointcut));
            else
            {
                try
                {
                    proxyOptions.Selector = new InstanceReference<CoreInterceptorSelector>(new CoreInterceptorSelector(proxyOptions.Selector.Resolve(kernel, null), Pointcut));
                }
                catch (Exception e)
                {
                    throw new Exception("can not change ProxyOptions selector for type : " + model.Name, e);
                }
            }
        }
    }
}