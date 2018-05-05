using System;

namespace Castle.Facilities.Aspect.SampleApp
{
    class Something : ISomething
    {
        [Log]
        public int Augment(int input)
        {
            return input + 1;
        }

        public void DoSomething(string input)
        {
            Console.WriteLine($"I'm doing something: {input}");
        }

        public int Property { get; set; }
    }
}