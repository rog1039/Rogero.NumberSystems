using FluentAssertions;
using Xunit;

namespace Rogero.NumberSystems.Tests
{
    public class TensTests
    {
        [Fact]
        public void BinaryTens()
        {
            var decimalResult = NumberConverter.ConvertFromDecimal(2, NumberSystem.Binary);
            decimalResult.Value.Should().Be("10");
        }


        [Fact]
        public void OctalTest()
        {
            var decimalResult = NumberConverter.ConvertFromDecimal(8, NumberSystem.Octal);
            decimalResult.Value.Should().Be("10");
        }

        [Fact]
        public void DecimalTens()
        {
            var decimalResult = NumberConverter.ConvertFromDecimal(10, NumberSystem.Decimal);
            decimalResult.Value.Should().Be("10");
        }

        [Fact]
        public void HexadecimalTests()
        {
            var decimalResult = NumberConverter.ConvertFromDecimal(16, NumberSystem.Hexadecimal);
            decimalResult.Value.Should().Be("10");
        }

        [Fact]
        public void AtoZTests()
        {
            var decimalResult = NumberConverter.ConvertFromDecimal(26, NumberSystem.AToZ_OneIndex);
            decimalResult.Value.Should().Be("Z");

            decimalResult = NumberConverter.ConvertFromDecimal(26, NumberSystem.AToZ_ZeroIndex);
            decimalResult.Value.Should().Be("AA");
        }
    }
}