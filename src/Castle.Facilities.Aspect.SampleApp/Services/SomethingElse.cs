using System;

namespace Castle.Facilities.Aspect.SampleApp
{
    class SomethingElse : ISomethingElse
    {
        public void DoSomethingElse(string input)
        {
            Console.WriteLine($"I'm doing something else and no proxy created: {input}");
        }
    }
}