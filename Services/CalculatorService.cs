using System;

namespace Dummy.Services
{
    public class CalculatorService : BaseService, ICalculatorService
    {
        public CalculatorService(ILoggingService logger) : base(logger) { }

        public int Add(int a, int b)
        {
            LogServiceCall($"Add({a}, {b})");
            return a + b;
        }

        public string Concat(string a, string b)
        {
            LogServiceCall($"Concat({a}, {b})");
            return a + b;
        }

        public double MultiplyAndAdd(double a, int b)
        {
            LogServiceCall($"MultiplyAndAdd({a}, {b})");
            return (a * 2) + b;
        }

        public string RepeatString(string s, int times)
        {
            LogServiceCall($"RepeatString({s}, {times})");
            if (times < 0) return string.Empty;
            return string.Concat(System.Linq.Enumerable.Repeat(s, times));
        }
    }
} 