using Castle.DynamicProxy;
using System;

namespace Castle.Facilities.Aspect
{
    public static class FacilityExtensions
    {
        public static AspectFacility AddAttributePointcut<T, TInterceptor>(this AspectFacility facility) where T : Attribute where TInterceptor : class, IInterceptor
        {
            facility.AddAspect(kernel => new AttributePointcut<T, TInterceptor>(kernel));

            return facility;
        }

        public static AspectFacility AddInheritancePointcut<T, TInterceptor>(this AspectFacility facility) where T : class where TInterceptor : class, IInterceptor
        {
            facility.AddAspect(kernel => new InheritancePointcut<T, TInterceptor>(kernel));

            return facility;
        }
    }
}
