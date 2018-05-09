using Castle.DynamicProxy;
using System;

namespace Castle.Facilities.Aspect
{
    public static class FacilityExtensions
    {
        /// <summary>
        /// Adds an attribute to be applied on an Advice/Interceptor
        /// </summary>
        /// <typeparam name="T">Generaly an Attribute to check for and can be of type AttributePointcut</typeparam>
        /// <typeparam name="TInterceptor">The actual implementation of the Advice/Interceptor to apply</typeparam>
        /// <param name="facility"></param>
        /// <returns></returns>
        public static AspectFacility AddAttributePointcut<T, TInterceptor>(this AspectFacility facility) where T : Attribute where TInterceptor : class, IInterceptor
        {
            facility.AddAspect(kernel => new AttributePointcut<T, TInterceptor>(kernel));

            return facility;
        }

        /// <summary>
        /// Adds a type to be applied on an Advice/Interceptor
        /// </summary>
        /// <typeparam name="T">Generaly the type that to check for any other assignable types and can be of type InheritancePointcut</typeparam>
        /// <typeparam name="TInterceptor">The actual implementation of the Advice/Interceptor to apply</typeparam>
        /// <param name="facility"></param>
        /// <returns></returns>
        public static AspectFacility AddInheritancePointcut<T, TInterceptor>(this AspectFacility facility) where T : class where TInterceptor : class, IInterceptor
        {
            facility.AddAspect(kernel => new InheritancePointcut<T, TInterceptor>(kernel));

            return facility;
        }
    }
}
