using Castle.Windsor;
using Castle.MicroKernel.Registration;

namespace Castle.Facilities.Aspect.SampleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = new WindsorContainer();

            container.Register(Component.For<ISomething>().ImplementedBy<Something>().LifeStyle.Singleton);
            container.Register(Component.For<ISomethingElse>().ImplementedBy<SomethingElse>().LifeStyle.Singleton);
            
            container.AddFacility<AspectFacility>(x =>
            {
                //Every method decorated with the LogAttribute will be intercepted
                x.AddAttributePointcut<LogAttribute, LogAdvice>();
                
                //Every type implementing the ISomething interface will be intercepted
                x.AddInheritancePointcut<ISomething, InhertiedLogAdvice>();
            });

            var something = container.Resolve<ISomething>();
            
            //It's going to get intercepted because of applied Log attribute
            something.Augment(10);

            //This is going to get intercepted too due to inherited pointcut declaration
            something.DoSomething("Hello World");


            var somethingElse = container.Resolve<ISomethingElse>();

            //No proxy object even created since no matching pointcut discovered
            somethingElse.DoSomethingElse("Bye World");
        }
    }
}