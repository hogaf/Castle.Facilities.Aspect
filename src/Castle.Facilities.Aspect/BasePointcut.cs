using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.Core;
using Castle.DynamicProxy;
using Castle.MicroKernel;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Naming;

namespace Castle.Facilities.Aspect
{
    public abstract class BasePointcut<TInterceptor> : IPointcut where TInterceptor : class, IInterceptor
    {
        protected IKernel Kernel { get; private set; }

        public BasePointcut(IKernel kernel)
        {
            Kernel = kernel;

            Kernel.Register(Component.For<TInterceptor>());
            var componentInspector = new BaseContributor(this);
            Kernel.ComponentModelBuilder.AddContributor(componentInspector);
            InspectPreviouslyRgisteredComponents(componentInspector);
        }

        /// <summary>
        /// inspecting previously registered components
        /// this might throw if components are configured in the wrong way
        /// </summary>
        /// <param name="componentInspector"></param>
        private void InspectPreviouslyRgisteredComponents(BaseContributor componentInspector)
        {
            ((INamingSubSystem) Kernel.GetSubSystem(SubSystemConstants.NamingKey))
                .GetAllHandlers()
                .Do(x => componentInspector.ProcessModel(Kernel, x.ComponentModel))
                .Run();
        }

        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            if (CanIntercept(type, method))
                if (interceptors.All(x => x.GetType() != typeof(TInterceptor)))
                {
                    interceptors = interceptors.Concat(new[] { Kernel.Resolve<TInterceptor>() }).ToArray();
                }

            return interceptors;
        }

        public void ApplyInterceptor(ComponentModel model)
        {
            if (!model.HasInterceptors && CanIntercept(model).Any())
            {
                Validate(model);
                model.Interceptors.AddIfNotInCollection(InterceptorReference.ForType(typeof (TInterceptor)));
            }
        }

        private void Validate(ComponentModel model)
        {
            List<string> problematicMethods;
            if (model.Services == null
                || model.Services.All(s => s.IsInterface)
                || (problematicMethods = (from method in CanIntercept(model)
                                          where !method.IsVirtual
                                          select method.Name).ToList()).Count == 0)
                return;

            throw new FacilityException(string.Format("The class {0} wants to use interception, " +
                                                      "however the methods must be marked as virtual in order to do so. Please correct " +
                                                      "the following methods: {1}", model.Implementation.FullName,
                                                      string.Join(", ", problematicMethods.ToArray())));
        }

        public abstract IEnumerable<MethodInfo> CanIntercept(ComponentModel model);

        public abstract bool CanIntercept(Type type, MethodInfo method);
    }
}
