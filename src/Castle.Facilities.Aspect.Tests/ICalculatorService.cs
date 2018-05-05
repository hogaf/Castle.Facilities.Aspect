using System;
using System.Collections.Generic;
using System.Text;

namespace Castle.Facilities.Aspect.Tests
{
    public interface ICalculatorService
    {
        int Divide(int x, int y);
        int Sum(int x, int y);
    }
}
