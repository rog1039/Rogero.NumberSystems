using FluentAssertions;
using Xunit;

namespace Rogero.NumberSystems.Tests;

public class AtoBTests
{
    [Fact]
    public void ZeroTest()
    {
        var convertAction = new Action(() => NumberConverter.ConvertFromDecimal(0, NumberSystem.AToB_OneIndex));
        convertAction.ShouldThrow<ArgumentException>();
    }

    [Fact]
    public void OneTest()
    {
        var result = NumberConverter.ConvertFromDecimal(1, NumberSystem.AToB_OneIndex);
        result.Value.Should().Be("A");
    }

    [Fact]
    public void TwoTest()
    {
        var result = NumberConverter.ConvertFromDecimal(2, NumberSystem.AToB_OneIndex);
        result.Value.Should().Be("B");
    }

    [Fact]
    public void ThreeTest()
    {
        var result = NumberConverter.ConvertFromDecimal(3, NumberSystem.AToB_OneIndex);
        result.Value.Should().Be("AA");
    }

    [Fact]
    public void FourTest()
    {
        var result = NumberConverter.ConvertFromDecimal(4, NumberSystem.AToB_OneIndex);
        result.Value.Should().Be("AB");
    }

    [Fact]
    public void FiveTest()
    {
        var result = NumberConverter.ConvertFromDecimal(5, NumberSystem.AToB_OneIndex);
        result.Value.Should().Be("BA");
    }

    [Fact]
    public void SixTest()
    {
        var result = NumberConverter.ConvertFromDecimal(6, NumberSystem.AToB_OneIndex);
        result.Value.Should().Be("BB");
    }

    [Fact]
    public void SevenTest()
    {
        var result = NumberConverter.ConvertFromDecimal(7, NumberSystem.AToB_OneIndex);
        result.Value.Should().Be("AAA");
    }

    [Fact]
    public void AlphabetTest()
    {
        var range = Enumerable.Range(1, 10).ToList();
        for (int i = range.First(); i < range.Count; i++)
        {
            var alphaValue = NumberConverter.ConvertFromDecimal(i, NumberSystem.AToB_OneIndex);
            Console.WriteLine($"{i,3} => {alphaValue}");
        }
    }
}