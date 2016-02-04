using FluentAssertions;
using Xunit;

namespace Rogero.NumberSystems.Tests
{
    public class TensTests
    {
        [Fact]
        public void BinaryTens()
        {
            var decimalResult = Converter.ConvertFromDecimal(2, NumberSystem.Binary);
            decimalResult.Value.Should().Be("10");
        }


        [Fact]
        public void OctalTest()
        {
            var decimalResult = Converter.ConvertFromDecimal(8, NumberSystem.Octal);
            decimalResult.Value.Should().Be("10");
        }

        [Fact]
        public void DecimalTens()
        {
            var decimalResult = Converter.ConvertFromDecimal(10, NumberSystem.Decimal);
            decimalResult.Value.Should().Be("10");
        }

        [Fact]
        public void HexadecimalTests()
        {
            var decimalResult = Converter.ConvertFromDecimal(16, NumberSystem.Hexadecimal);
            decimalResult.Value.Should().Be("10");
        }

        [Fact]
        public void AtoZTests()
        {
            var decimalResult = Converter.ConvertFromDecimal(26, NumberSystem.AToZ_OneIndex);
            decimalResult.Value.Should().Be("Z");

            decimalResult = Converter.ConvertFromDecimal(26, NumberSystem.AToZ_ZeroIndex);
            decimalResult.Value.Should().Be("AA");
        }
    }
}