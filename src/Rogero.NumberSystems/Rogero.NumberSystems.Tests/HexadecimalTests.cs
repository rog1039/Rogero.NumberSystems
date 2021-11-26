using Xunit;

namespace Rogero.NumberSystems.Tests;

public class HexadecimalTests
{
    [Fact]
    public void BigRangeMap()
    {
        var range = Enumerable.Range(0, 1000).ToList();
        for (int i = 0; i < range.Count; i++)
        {
            var binary = NumberConverter.ConvertFromDecimal(i, NumberSystem.Hexadecimal);
            Console.WriteLine($"{i,4} => {binary}");
        }
    }
}