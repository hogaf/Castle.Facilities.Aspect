using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Castle.Facilities.Aspect.Tests
{
    public class CounterAdvice : IInterceptor
    {
        private int _counter;
        public int Counter
        {
            get { return _counter; }
        }

        public void Intercept(IInvocation invocation)
        {
            _counter++;
            invocation.Proceed();
        }
    }
}
