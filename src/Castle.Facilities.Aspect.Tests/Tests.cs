using Castle.MicroKernel.Registration;
using NUnit.Framework;

namespace Castle.Facilities.Aspect.Tests
{
    [TestFixture]
    public class Tests : AbstratTestCase
    {
        [Test]
        public void Basic_Usage_Using_Facility()
        {
            Container.AddFacility<AspectFacility>(x =>
            {
                x.AddAttributePointcut<PlusOneAttribute, PlusOneAdvice>();
            });

            RunTest();
        }

        [Test]
        public void Basic_Usage_Without_Using_Facility()
        {
            Container.Register(Component
                .For<IPointcut>()
                .Instance(new AttributePointcut<PlusOneAttribute, PlusOneAdvice>(Container.Kernel)));

            RunTest();
        }

        public void RunTest()
        {
            var calculator = Container.Resolve<ICalculatorService>();

            var result = calculator.Sum(10, 20);

            Assert.AreEqual(result, 31);
        }
    }
}