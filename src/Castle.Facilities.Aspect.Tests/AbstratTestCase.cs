using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace Castle.Facilities.Aspect.Tests
{
    public abstract class AbstratTestCase
    {
        protected IWindsorContainer Container { get; private set; }

        [SetUp]
        public void Init()
        {
            Container = BuildContainer();
            Container.Register(Component.For<ICalculatorService>().ImplementedBy<CalculatorService>().LifeStyle.Transient);
        }

        [TearDown]
        public void CleanUp()
        {
            Container.Dispose();
        }

        protected virtual WindsorContainer BuildContainer()
        {
            return new WindsorContainer();
        }

        protected void ResetContainer()
        {
            CleanUp();
            Init();
        }
    }
}
