using System;
using NCrunch.Framework;
using Xunit;

namespace Rogero.NumberSystems.Tests
{
    public class NonCollapsibleToDecimalConverter
    {
        [Timeout(8000)]
        [Fact()]
        [Trait("Category", "Instant")]
        public void BigTable()
        {
            PrintTables(1, 3257,
                        NumberSystem.Decimal,
                        NumberSystem.Binary,
                        NumberSystem.BinaryNoCollapse,
                        NumberSystem.Trinary,
                        NumberSystem.TrinaryNoCollapse,
                        NumberSystem.Octal,
                        NumberSystem.Hexadecimal,
                        NumberSystem.AToB_ZeroIndex,
                        NumberSystem.AToB_OneIndex,
                        NumberSystem.AToZ_ZeroIndex,
                        NumberSystem.AToZ_OneIndex);
        }

        private void PrintTables(int start, int end, params NumberSystem[] numberSystems)
        {
            string[,] cells = new string[end - start + 1, 1 + numberSystems.Length];
            for (int i = 0; i <= end - start; i++)
            {
                cells[i, 0] = (start + i - 1).ToString();
                for (int j = 0; j < numberSystems.Length; j++)
                {
                    var numberSystem = numberSystems[j];
                    if (i == 0)
                    {
                        cells[i, j + 1] = numberSystem.ToString();
                    }
                    else
                    {
                        var decimalNumber = (start + i - 1).ToString();
                        var numberInSystem = NumberConverter.Convert(decimalNumber, NumberSystem.Decimal, numberSystem);
                        var decimalNumberBack = numberInSystem.ToDecimal();
                        cells[i, j + 1] = decimalNumberBack.Value;
                    }
                }
            }
            var output = cells.ToStringTable();
            Console.WriteLine(output);
        }
    }


    public class RandomAlphabetListings
    {
        [Fact()]
        [Trait("Category", "Instant")]
        public void Binary_Collapse_WithZero()
        {
            Print(NumberSystem.Binary, 0, 50);
        }
        [Fact()]
        [Trait("Category", "Instant")]
        public void Binary_NoCollapse_WithZero()
        {
            Print(NumberSystem.BinaryNoCollapse, 0, 50);
        }

        [Fact()]
        [Trait("Category", "Instant")]
        public void Trinary_Collapse_WithZero()
        {
            Print(NumberSystem.Trinary, 0, 50);
        }
        [Fact()]
        [Trait("Category", "Instant")]
        public void Trinary_NoCollapse_WithZero()
        {
            Print(NumberSystem.TrinaryNoCollapse, 0, 50);
        }

        private void Print(NumberSystem numberSystem, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                var number = NumberConverter.ConvertFromDecimal(i, numberSystem);
                Console.WriteLine("{0,6} {1,6}  - [{2}]", i, number.Value, number.NumberSystem);
            }
        }

        [Timeout(8000)]
        [Fact()]
        [Trait("Category", "Instant")]
        public void Bi_Tri_Tables_Large()
        {
            PrintTables(1, 3257,
                        NumberSystem.Decimal,
                        NumberSystem.Binary,
                        NumberSystem.BinaryNoCollapse,
                        NumberSystem.Trinary,
                        NumberSystem.TrinaryNoCollapse,
                        NumberSystem.Octal,
                        NumberSystem.Hexadecimal,
                        NumberSystem.AToB_ZeroIndex,
                        NumberSystem.AToB_OneIndex,
                        NumberSystem.AToZ_ZeroIndex,
                        NumberSystem.AToZ_OneIndex);
        }

        [Fact()]
        [Trait("Category", "Instant")]
        public void Bi_Tri_Tables_Small()
        {
            PrintTables(1, 28,
                        NumberSystem.Decimal,
                        NumberSystem.Binary,
                        NumberSystem.BinaryNoCollapse,
                        NumberSystem.Trinary,
                        NumberSystem.TrinaryNoCollapse,
                        NumberSystem.Octal,
                        NumberSystem.Hexadecimal,
                        NumberSystem.AToA_ZeroIndex,
                        NumberSystem.AToA_OneIndex,
                        NumberSystem.AToB_ZeroIndex,
                        NumberSystem.AToB_OneIndex,
                        NumberSystem.AToZ_ZeroIndex,
                        NumberSystem.AToZ_OneIndex);
        }

        private void PrintTables(int start, int end, params NumberSystem[] numberSystems)
        {
            string[,] cells = new string[end - start + 1, 1 + numberSystems.Length];
            for (int i = 0; i <= end - start; i++)
            {
                cells[i, 0] = (start + i-1).ToString();
                for (int j = 0; j < numberSystems.Length; j++)
                {
                    var numberSystem = numberSystems[j];
                    if (i == 0)
                    {
                        cells[i, j + 1] = numberSystem.ToString();
                    }
                    else
                    {
                        var number = (start + i-1).ToString();
                        var value = NumberConverter.Convert(number, NumberSystem.Decimal, numberSystem).Value;
                        cells[i, j + 1] = value;
                    }
                }
            }
            var output = cells.ToStringTable();
            Console.WriteLine(output);
        }
    }
}