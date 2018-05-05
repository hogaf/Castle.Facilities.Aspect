using NUnit.Framework;

namespace Castle.Facilities.Aspect.Tests
{
    [TestFixture]
    public class InhertitanceTests : AbstratTestCase
    {
        [Test]
        public void Proxy_Would_Not_Be_Created_For_No_Matching_Pointcut()
        {
            Container.AddFacility<AspectFacility>(x =>
            {
                x.AddInheritancePointcut<IMultiplierService, CounterAdvice>();
            });

            var calculator = Container.Resolve<ICalculatorService>();

            Assert.AreEqual(calculator.GetType(), typeof(CalculatorService));
        }

        [Test]
        public void Inheritance_Advice_Would_Intercept_Two_Method_Calls()
        {
            Container.AddFacility<AspectFacility>(x =>
            {
                x.AddInheritancePointcut<ICalculatorService, CounterAdvice>();
            });

            var calculator = Container.Resolve<ICalculatorService>();

            calculator.Sum(10, 20);
            calculator.Divide(10, 20);

            var advice = Container.Resolve<CounterAdvice>();
            Assert.AreEqual(advice.Counter, 2);
        }
    }
}