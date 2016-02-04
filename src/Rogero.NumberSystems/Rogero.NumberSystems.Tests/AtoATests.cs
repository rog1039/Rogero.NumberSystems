using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Rogero.NumberSystems.Tests
{
    public class AtoATests
    {
        [Fact]
        public void ZeroTest()
        {
            var convertAction = new Action(() => Converter.ConvertFromDecimal(0, NumberSystem.AToA_OneIndex));
            convertAction.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void OneTest()
        {
            var result = Converter.ConvertFromDecimal(1, NumberSystem.AToA_OneIndex);
            result.Value.Should().Be("A");
        }


        [Fact]
        public void TwoTest()
        {
            var result = Converter.ConvertFromDecimal(2, NumberSystem.AToA_OneIndex);
            result.Value.Should().Be("AA");
        }

        [Fact]
        public void AlphabetTest()
        {
            var range = Enumerable.Range(1, 10).ToList();
            for (int i = range.First(); i < range.Count; i++)
            {
                var alphaValue = Converter.ConvertFromDecimal(i, NumberSystem.AToA_OneIndex);
                Console.WriteLine($"{i,3} => {alphaValue}");
            }
        }
    }
}