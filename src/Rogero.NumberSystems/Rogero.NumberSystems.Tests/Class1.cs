using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }

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
            var decimalResult = Converter.ConvertFromDecimal(26, NumberSystem.AToZ);
            decimalResult.Value.Should().Be("AA");
        }
    }

    public class AtoZTests
    {
        [Fact]
        public void AlphabetTest()
        {
            var input = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Select(ch => ch.ToString()).ToList();
            input.AddRange(new[] {"AA", "AB", "AC"});

            for (int i = 0; i < input.Count; i++)
            {
                var alpha = Converter.ConvertFromDecimal(i, NumberSystem.AToZ);
                var expected = input[i];
                Console.WriteLine($"{i,2}) {expected} => {alpha}");
                alpha.Value.Should().Be(expected);
            }
        }
        
        [Fact]
        public void BigRangeTest()
        {
            var range = Enumerable.Range(0, 2000).ToList();
            for (int i = 0; i < range.Count; i++)
            {
                var alphaValue = Converter.ConvertFromDecimal(i, NumberSystem.AToZ);
                Console.WriteLine($"{i,3} => {alphaValue}");
            }
        }

        [Fact]
        public void HundredsTest()
        {
            var zz = Converter.ConvertFromDecimal(26*26 - 1, NumberSystem.AToZ);
            var aaa = Converter.ConvertFromDecimal(26 * 26, NumberSystem.AToZ);

            Console.WriteLine($"26*26-1 => ZZ  ?? {zz}");
            Console.WriteLine($"26*26   => AAA ?? {aaa}");

            zz.Value.Should().Be("ZZ");
            aaa.Value.Should().Be("AAA");
        }
    }

    public class AtoATests
    {
        [Fact]
        public void ZeroTest()
        {
            var convertAction = new Action(() => Converter.ConvertFromDecimal(0, NumberSystem.AToA));
            convertAction.ShouldThrow<IndexOutOfRangeException>();
        }

        [Fact]
        public void OneTest()
        {
            var result = Converter.ConvertFromDecimal(1, NumberSystem.AToA);
            result.Value.Should().Be("A");
        }


        [Fact]
        public void TwoTest()
        {
            var result = Converter.ConvertFromDecimal(2, NumberSystem.AToA);
            result.Value.Should().Be("AA");
        }

        [Fact]
        public void AlphabetTest()
        {
            var range = Enumerable.Range(1, 10).ToList();
            for (int i = 0; i < range.Count; i++)
            {
                var alphaValue = Converter.ConvertFromDecimal(i, NumberSystem.AToA);
                Console.WriteLine($"{i,3} => {alphaValue}");
            }
        }
    }
}
