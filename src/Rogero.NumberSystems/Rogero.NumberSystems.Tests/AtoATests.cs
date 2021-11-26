using FluentAssertions;
using Xunit;

namespace Rogero.NumberSystems.Tests;

public class AtoATests
{
    [Fact]
    public void ZeroTest()
    {
        var convertAction = new Action(() => NumberConverter.ConvertFromDecimal(0, NumberSystem.AToA_OneIndex));
        convertAction.ShouldThrow<ArgumentException>();
    }

    [Fact]
    public void OneTest()
    {
        var result = NumberConverter.ConvertFromDecimal(1, NumberSystem.AToA_OneIndex);
        result.Value.Should().Be("A");
    }


    [Fact]
    public void TwoTest()
    {
        var result = NumberConverter.ConvertFromDecimal(2, NumberSystem.AToA_OneIndex);
        result.Value.Should().Be("AA");
    }

    [Fact]
    public void AlphabetTest()
    {
        var range = Enumerable.Range(1, 10).ToList();
        for (int i = range.First(); i < range.Count; i++)
        {
            var alphaValue = NumberConverter.ConvertFromDecimal(i, NumberSystem.AToA_OneIndex);
            Console.WriteLine($"{i,3} => {alphaValue}");
        }
    }
}