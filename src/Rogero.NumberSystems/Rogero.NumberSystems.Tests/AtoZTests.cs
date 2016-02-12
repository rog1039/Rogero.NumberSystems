using System;
using System.Linq;
using Xunit;

namespace Rogero.NumberSystems.Tests
{
    public class AtoZTests
    {
        [Fact]
        public void BigRangeTest()
        {
            var range = Enumerable.Range(0, 2000).ToList();
            for (int i = 1; i < range.Count; i++)
            {
                var alphaValue = NumberConverter.ConvertFromDecimal(i, NumberSystem.AToZ_OneIndex);
                Console.WriteLine($"{i,3} => {alphaValue}");
            }
        }
    }
}