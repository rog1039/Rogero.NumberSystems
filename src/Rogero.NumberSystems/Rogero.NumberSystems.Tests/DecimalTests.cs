using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Rogero.NumberSystems.Tests
{
    public class DecimalTests
    {
        [Fact]
        public void TenIs10()
        {
            var decimalResult = Converter.ConvertFromDecimal(10, NumberSystem.Decimal);
            decimalResult.Value.Should().Be("10");
        }

        [Fact]
        public void BigRangeMap()
        {
            var range = Enumerable.Range(0, 2001).ToList();
            for (int i = 0; i < range.Count; i++)
            {
                var binary = Converter.ConvertFromDecimal(i, NumberSystem.Decimal);
                Console.WriteLine($"{i,4} => {binary}");
            }
        }

        [Fact]
        public void ParseToDecimal()
        {
            for (int i = 0; i < 2500; i++)
            {
                var numberAsString = i.ToString();
                var parsedNumber = Converter.Convert(numberAsString, NumberSystem.Decimal, NumberSystem.Decimal);
                numberAsString.Should().Be(parsedNumber.Value);
            }
        }
    }
}
