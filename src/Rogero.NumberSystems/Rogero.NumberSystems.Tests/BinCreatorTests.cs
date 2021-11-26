using FluentAssertions;
using Xunit;

namespace Rogero.NumberSystems.Tests;

public class BinCreatorTests
{
    [Fact()]
    [Trait("Category", "Instant")]
    public void PrintMaintenanceBins()
    {
        var subBins = Helpers.GetStringList(1,   8);
        var shelves = Helpers.GetStringList(1,   7);
        var columns = Helpers.GetStringList(1,   7);
        var rows    = Helpers.GetStringList("J", "J");

        var result = CompoundNumberEnumerator.Enumerate(rows, columns, shelves, subBins);
        Console.WriteLine(result.Count);


        //Remove existing...


        var newBins = result.Where(z => !ExistingBins.Existing.Contains($"MU{z[0]}{z[1]}.{z[2]}.{z[3]}")).ToList();
        Console.WriteLine(newBins.Count);

        PrintResult(newBins);
    }

    [Fact()]
    [Trait("Category", "Instant")]
    public void SimpleOneWide2Bins()
    {
        var bin = Helpers.GetStringList(1, 2);
    }

    [Fact()]
    [Trait("Category", "Instant")]
    public void SimpleTwoWide1Bin()
    {
        var comp2  = Helpers.GetStringList(1,   1);
        var comp1  = Helpers.GetStringList("a", "a");
        var result = CompoundNumberEnumerator.Enumerate(comp1, comp2);
        result.Count.Should().Be(1);
    }

    [Fact()]
    [Trait("Category", "Instant")]
    public void SimpleTwoWide2Bin()
    {
        var comp2  = Helpers.GetStringList(1,   2);
        var comp1  = Helpers.GetStringList("a", "b");
        var result = CompoundNumberEnumerator.Enumerate(comp1, comp2);
        result.Count.Should().Be(4);
        PrintResult(result);
    }

    private void PrintResult(IList<IList<string>> results)
    {
        foreach (var result in results)
        {
            var output = string.Join(", ", result);
            Console.WriteLine(output);
        }
    }
}

public static class Helpers
{
    public static List<string> GetStringList(int min, int max)
    {
        var range = Enumerable.Range(min, max).Select(z => z.ToString()).ToList();
        return range;
    }

    public static List<string> GetStringList(string minLetter, string maxLetter)
    {
        var min   = char.ConvertToUtf32(minLetter, 0);
        var max   = char.ConvertToUtf32(maxLetter, 0);
        var array = new string[max - min+1];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = char.ConvertFromUtf32(min + i);
        }
        return array.ToList();
    }
}

public static class CompoundNumberEnumerator
{
    public static IList<IList<string>> Enumerate(params IList<string>[] inputLists)
    {
        var maximums = inputLists.Select(z => z.Count);
        var counter  = new CompoundCounter(maximums);
        if (counter.TotalCombinations == 0) return new List<IList<string>>();
        var result = (IList<IList<string>>) new List<IList<string>>();

        do
        {
            var currentIndices = counter.Current;
            var currentValue   = (IList<string>)currentIndices.Select((i, listIndex) => inputLists[listIndex][i]).ToList();
            result.Add(currentValue);

        } while (counter.Increment());

        return result;
    }
}

public class CompoundCounter
{
    public IList<int> Maximums = new List<int>();
    public IList<int> Current  = new List<int>();
    public int        TotalCombinations => Maximums.Aggregate(1, (max, i) => (max) * i);
        
    public CompoundCounter(params int[] counts)
    {
        foreach (var count in counts)
        {
            Maximums.Add(count);
            Current.Add(0);
        }
    }

    public CompoundCounter(IEnumerable<int> counts) : this(counts.ToArray()) { }

    public bool Increment()
    {
        for (int i = Maximums.Count - 1; i >= 0; i--)
        {
            var current      = Current[i];
            var maximum      = Maximums[i];
            var canIncrement = (current+1) < maximum;
            if (canIncrement)
            {
                Current[i]++;
                return true;
            }
            else
            {
                //Reset previous numbers
                for (int j = Maximums.Count - 1; j >= i; j--)
                {
                    Current[j] = 0;
                }

                var atLastNumber = i == 0;
                if (atLastNumber) return false;
            }
        }
        return false;
    }

    public override string ToString()
    {
        var outp   = String.Join(", ", Current);
        var output = Current.Aggregate("", (input, i) => $"{input}, {i.ToString()}");
        return outp;
    }
}

public class ComplexCounterTests
{
    public void PrintAll(CompoundCounter counter)
    {
        Console.WriteLine(counter);
        while (counter.Increment())
        {
            Console.WriteLine(counter);
        }
    }

    [Fact()]
    [Trait("Category", "Instant")]
    public void CounterOf1()
    {
        var counter = new CompoundCounter(1);
        counter.TotalCombinations.Should().Be(1);
        PrintAll(counter);
    }

    [Fact()]
    [Trait("Category", "Instant")]
    public void CounterOf2()
    {
        var counter = new CompoundCounter(2);
        counter.TotalCombinations.Should().Be(2);
        PrintAll(counter);
    }

    [Fact()]
    [Trait("Category", "Instant")]
    public void CounterOf16()
    {
        var counter = new CompoundCounter(16);
        counter.TotalCombinations.Should().Be(16);
        PrintAll(counter);
    }

    [Fact()]
    [Trait("Category", "Instant")]
    public void CounterOf1_1()
    {
        var counter = new CompoundCounter(1, 1);
        counter.TotalCombinations.Should().Be(1);
        PrintAll(counter);
    }

    [Fact()]
    [Trait("Category", "Instant")]
    public void CounterOf2_2()
    {
        var counter = new CompoundCounter(2, 2);
        counter.TotalCombinations.Should().Be(4);
        PrintAll(counter);
    }

    [Fact()]
    [Trait("Category", "Instant")]
    public void CounterOf2_2_2()
    {
        var counter = new CompoundCounter(2, 2, 2);
        counter.TotalCombinations.Should().Be(8);
        PrintAll(counter);
    }

    [Fact()]
    [Trait("Category", "Instant")]
    public void CounterOf5_5_5()
    {
        var counter = new CompoundCounter(5, 5, 5, 5);
        counter.TotalCombinations.Should().Be(625);
        PrintAll(counter);
    }
}