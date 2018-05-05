using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;

namespace Castle.Facilities.Aspect
{
    /// <summary>
    /// Castle facility for Aspect
    /// </summary>
    /// <seealso cref="Castle.MicroKernel.Facilities.AbstractFacility" />
    public class AspectFacility : AbstractFacility
    {
        private IList<Func<IKernel, IPointcut>> _registrations;

        protected IList<Func<IKernel, IPointcut>> Registrations => _registrations ?? (_registrations = new List<Func<IKernel, IPointcut>>());

        protected override void Init()
        {
            foreach (var instanceCreator in Registrations)
            {
                Kernel.Register(Component
               .For<IPointcut>()
               .Instance(instanceCreator(Kernel)));
            }
        }

        public AspectFacility AddAspect(Func<IKernel, IPointcut> instanceCreator)
        {
            Registrations.Add(instanceCreator);

            return this;
        }
    }
}