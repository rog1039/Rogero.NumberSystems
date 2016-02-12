using System;
using System.Linq;
using Xunit;

namespace Rogero.NumberSystems.Tests
{
    public class BinaryTests
    {
        [Fact]
        public void BigRangeMap()
        {
            var range = Enumerable.Range(0, 1000).ToList();
            for (int i = 0; i < range.Count; i++)
            {
                var binary = NumberConverter.ConvertFromDecimal(i, NumberSystem.Binary);
                Console.WriteLine($"{i,4} => {binary}");
            }
        }

        [Fact]
        public void BigRangeMapNoCollapse()
        {
            var range = Enumerable.Range(0, 20).ToList();
            for (int i = 0; i < range.Count; i++)
            {
                var binary = NumberConverter.ConvertFromDecimal(i, NumberSystem.BinaryNoCollapse);
                Console.WriteLine($"{i,4} => {binary}");
            }
            for (int i = 0; i < range.Count; i++)
            {
                var binary = NumberConverter.ConvertFromDecimal(i, NumberSystem.TrinaryNoCollapse);
                Console.WriteLine($"{i,4} => {binary}");
            }
        }


        [Fact]
        public void BigRangeMapNoCollapseTrinary()
        {
            var range = Enumerable.Range(0, 30).ToList();
            for (int i = 0; i < range.Count; i++)
            {
                var binary = NumberConverter.ConvertFromDecimal(i, NumberSystem.TrinaryNoCollapse);
                Console.WriteLine($"{i,4} => {binary}");
            }
        }
    }
}