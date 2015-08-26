using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

    public class BinaryTests
    {
        [Fact]
        public void BigRangeMap()
        {
            var range = Enumerable.Range(0, 1000).ToList();
            for (int i = 0; i < range.Count; i++)
            {
                var binary = Converter.ConvertFromDecimal(i, NumberSystem.Binary);
                Console.WriteLine($"{i,4} => {binary}");
            }
        }
    }
    
    public class OctalTests
    {
        [Fact]
        public void BigRangeMap()
        {
            var range = Enumerable.Range(0, 1000).ToList();
            for (int i = 0; i < range.Count; i++)
            {
                var binary = Converter.ConvertFromDecimal(i, NumberSystem.Octal);
                Console.WriteLine($"{i,4} => {binary}");
            }
        }
    }

    public class HexadecimalTests
    {
        [Fact]
        public void BigRangeMap()
        {
            var range = Enumerable.Range(0, 1000).ToList();
            for (int i = 0; i < range.Count; i++)
            {
                var binary = Converter.ConvertFromDecimal(i, NumberSystem.Hexadecimal);
                Console.WriteLine($"{i,4} => {binary}");
            }
        }
    }

    public class AtoATests
    {
        [Fact]
        public void ZeroTest()
        {
            var convertAction = new Action(() => Converter.ConvertFromDecimal(0, NumberSystem.AToA));
            convertAction.ShouldThrow<ArgumentException>();
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
            for (int i = range.First(); i < range.Count; i++)
            {
                var alphaValue = Converter.ConvertFromDecimal(i, NumberSystem.AToA);
                Console.WriteLine($"{i,3} => {alphaValue}");
            }
        }
    }

    public class AtoBTests
    {
        [Fact]
        public void ZeroTest()
        {
            var convertAction = new Action(() => Converter.ConvertFromDecimal(0, NumberSystem.AToB));
            convertAction.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void OneTest()
        {
            var result = Converter.ConvertFromDecimal(1, NumberSystem.AToB);
            result.Value.Should().Be("A");
        }
        
        [Fact]
        public void TwoTest()
        {
            var result = Converter.ConvertFromDecimal(2, NumberSystem.AToB);
            result.Value.Should().Be("B");
        }

        [Fact]
        public void ThreeTest()
        {
            var result = Converter.ConvertFromDecimal(3, NumberSystem.AToB);
            result.Value.Should().Be("AA");
        }

        [Fact]
        public void FourTest()
        {
            var result = Converter.ConvertFromDecimal(4, NumberSystem.AToB);
            result.Value.Should().Be("AB");
        }

        [Fact]
        public void FiveTest()
        {
            var result = Converter.ConvertFromDecimal(5, NumberSystem.AToB);
            result.Value.Should().Be("BA");
        }

        [Fact]
        public void SixTest()
        {
            var result = Converter.ConvertFromDecimal(6, NumberSystem.AToB);
            result.Value.Should().Be("BB");
        }

        [Fact]
        public void SevenTest()
        {
            var result = Converter.ConvertFromDecimal(7, NumberSystem.AToB);
            result.Value.Should().Be("AAA");
        }

        [Fact]
        public void AlphabetTest()
        {
            var range = Enumerable.Range(1, 10).ToList();
            for (int i = range.First(); i < range.Count; i++)
            {
                var alphaValue = Converter.ConvertFromDecimal(i, NumberSystem.AToB);
                Console.WriteLine($"{i,3} => {alphaValue}");
            }
        }
    }
}
