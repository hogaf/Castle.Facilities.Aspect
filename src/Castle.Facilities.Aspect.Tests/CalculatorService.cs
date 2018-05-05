using System;
using System.Collections.Generic;
using System.Text;

namespace Castle.Facilities.Aspect.Tests
{
    public class CalculatorService : ICalculatorService
    {
        public int Divide(int x, int y)
        {
            return x / y;
        }

        [PlusOne]
        public int Sum(int x, int y)
        {
            return x + y;
        }
    }
}
