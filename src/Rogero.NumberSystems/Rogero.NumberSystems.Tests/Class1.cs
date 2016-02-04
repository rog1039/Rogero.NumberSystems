using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
            var  decimalResult = Converter.ConvertFromDecimal(26, NumberSystem.AToZ_OneIndex);
            decimalResult.Value.Should().Be("Z");

            decimalResult = Converter.ConvertFromDecimal(26, NumberSystem.AToZ_ZeroIndex);
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
                var alpha = Converter.ConvertFromDecimal(i, NumberSystem.AToZ_OneIndex);
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
                var alphaValue = Converter.ConvertFromDecimal(i, NumberSystem.AToZ_OneIndex);
                Console.WriteLine($"{i,3} => {alphaValue}");
            }
        }

        [Fact]
        public void HundredsTest()
        {
            var zz = Converter.ConvertFromDecimal(26 * 26 - 1, NumberSystem.AToZ_OneIndex);
            var aaa = Converter.ConvertFromDecimal(26 * 26, NumberSystem.AToZ_OneIndex);

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

        [Fact]
        public void BigRangeMapNoCollapse()
        {
            var range = Enumerable.Range(0, 20).ToList();
            for (int i = 0; i < range.Count; i++)
            {
                var binary = Converter.ConvertFromDecimal(i, NumberSystem.BinaryNoCollapse);
                Console.WriteLine($"{i,4} => {binary}");
            }
            for (int i = 0; i < range.Count; i++)
            {
                var binary = Converter.ConvertFromDecimal(i, NumberSystem.TrinaryNoCollapse);
                Console.WriteLine($"{i,4} => {binary}");
            }
        }


        [Fact]
        public void BigRangeMapNoCollapseTrinary()
        {
            var range = Enumerable.Range(0, 30).ToList();
            for (int i = 0; i < range.Count; i++)
            {
                var binary = Converter.ConvertFromDecimal(i, NumberSystem.TrinaryNoCollapse);
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
                var number = Converter.ConvertFromDecimal(i, numberSystem);
                Console.WriteLine("{0,6} {1,6}  - [{2}]", i, number.Value, number.NumberSystem.Description);
            }
        }

        [Fact()]
        [Trait("Category", "Instant")]
        public void Bi_Tri_Tables_Large()
        {
            PrintTables(0, 257, NumberSystem.Binary, 
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
            PrintTables(0, 28, NumberSystem.Binary,
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
            string[,] cells = new string[end - start+1, 1 + numberSystems.Length];
            for (int i = 0; i <= end-start; i++)
            {
                cells[i, 0] = (start+i).ToString();
                for (int j = 0; j < numberSystems.Length; j++)
                {
                    cells[i, j+1] = Converter.ConvertFromDecimal(start + i, numberSystems[j]).Value;
                }
            }
            var output = cells.ToStringTable();
            Console.WriteLine(output);
        }
    }

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

    public class AtoBTests
    {
        [Fact]
        public void ZeroTest()
        {
            var convertAction = new Action(() => Converter.ConvertFromDecimal(0, NumberSystem.AToB_OneIndex));
            convertAction.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void OneTest()
        {
            var result = Converter.ConvertFromDecimal(1, NumberSystem.AToB_OneIndex);
            result.Value.Should().Be("A");
        }
        
        [Fact]
        public void TwoTest()
        {
            var result = Converter.ConvertFromDecimal(2, NumberSystem.AToB_OneIndex);
            result.Value.Should().Be("B");
        }

        [Fact]
        public void ThreeTest()
        {
            var result = Converter.ConvertFromDecimal(3, NumberSystem.AToB_OneIndex);
            result.Value.Should().Be("AA");
        }

        [Fact]
        public void FourTest()
        {
            var result = Converter.ConvertFromDecimal(4, NumberSystem.AToB_OneIndex);
            result.Value.Should().Be("AB");
        }

        [Fact]
        public void FiveTest()
        {
            var result = Converter.ConvertFromDecimal(5, NumberSystem.AToB_OneIndex);
            result.Value.Should().Be("BA");
        }

        [Fact]
        public void SixTest()
        {
            var result = Converter.ConvertFromDecimal(6, NumberSystem.AToB_OneIndex);
            result.Value.Should().Be("BB");
        }

        [Fact]
        public void SevenTest()
        {
            var result = Converter.ConvertFromDecimal(7, NumberSystem.AToB_OneIndex);
            result.Value.Should().Be("AAA");
        }

        [Fact]
        public void AlphabetTest()
        {
            var range = Enumerable.Range(1, 10).ToList();
            for (int i = range.First(); i < range.Count; i++)
            {
                var alphaValue = Converter.ConvertFromDecimal(i, NumberSystem.AToB_OneIndex);
                Console.WriteLine($"{i,3} => {alphaValue}");
            }
        }
    }

    
    public static class TableParserExtensions
    {
        public static void PrintStringTable<T>(this IEnumerable<T> values)
        {
            Console.WriteLine(values.ToStringTable());
        }
        public static string ToStringTable<T>(this IEnumerable<T> values)
        {
            var objectProperties = GetObjectProperties<T>(values);
            var columnHeaders = new string[objectProperties.Length];
            var valueSelectors = new Func<T, object>[objectProperties.Length];

            for (int i = 0; i < objectProperties.Count(); i++)
            {
                var propertyInfo = objectProperties[i];
                var columnHeader = propertyInfo.Name;
                var valueSelector = new Func<T, object>(input => propertyInfo.GetValue(input, null));

                columnHeaders[i] = columnHeader;
                valueSelectors[i] = valueSelector;
            }

            return ToStringTable(values, columnHeaders, valueSelectors);
        }

        private static PropertyInfo[] GetObjectProperties<T>(IEnumerable<T> values)
        {
            //Can't use the code below since it may very well be that T is object and the list contains subtypes of object
            //We must use it though if the values array has no elements.
            if (!values.Any())
            {
                var objectProperties = typeof (T).GetProperties();
                return objectProperties;
            }

            //So instead, let's check the type of the first element
            var type = values.First().GetType();
            return type.GetProperties();
        }

        public static string ToStringTable<T>(this IEnumerable<T> values, string[] columnHeaders, params Func<T, object>[] valueSelectors)
        {
            return ToStringTable(values.ToArray(), columnHeaders, valueSelectors);
        }

        public static string ToStringTable<T>(this T[] values, string[] columnHeaders, params Func<T, object>[] valueSelectors)
        {
            Debug.Assert(columnHeaders.Length == valueSelectors.Length);

            var arrValues = new string[values.Length + 1, valueSelectors.Length];

            // Fill headers
            for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            {
                arrValues[0, colIndex] = columnHeaders[colIndex];
            }

            // Fill table rows
            for (int rowIndex = 1; rowIndex < arrValues.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
                {
                    object value = valueSelectors[colIndex].Invoke(values[rowIndex - 1]);

                    arrValues[rowIndex, colIndex] = value != null ? value.ToString() : "null";
                }
            }

            return ToStringTable(arrValues);
        }

        public static string ToStringTable(this string[,] arrValues)
        {
            int[] maxColumnsWidth = GetMaxColumnsWidth(arrValues);
            var headerSpliter = new string('-', maxColumnsWidth.Sum(i => i + 3) - 1);

            var sb = new StringBuilder();
            for (int rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
                {
                    // Print cell
                    string cell = arrValues[rowIndex, colIndex];
                    cell = cell.PadRight(maxColumnsWidth[colIndex]);
                    sb.Append(" | ");
                    sb.Append(cell);
                }

                // Print end of line
                sb.Append(" | ");
                sb.AppendLine();

                // Print splitter
                if (rowIndex == 0)
                {
                    sb.AppendFormat(" |{0}| ", headerSpliter);
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        private static int[] GetMaxColumnsWidth(string[,] arrValues)
        {
            var maxColumnsWidth = new int[arrValues.GetLength(1)];
            for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            {
                for (int rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
                {
                    int newLength = arrValues[rowIndex, colIndex].Length;
                    int oldLength = maxColumnsWidth[colIndex];

                    if (newLength > oldLength)
                    {
                        maxColumnsWidth[colIndex] = newLength;
                    }
                }
            }

            return maxColumnsWidth;
        }

        public static string ToStringTable<T>(this IEnumerable<T> values, params Expression<Func<T, object>>[] valueSelectors)
        {
            var headers = valueSelectors.Select(func => GetProperty(func).Name).ToArray();
            var selectors = valueSelectors.Select(exp => exp.Compile()).ToArray();
            return ToStringTable(values, headers, selectors);
        }
        public static void PrintStringTable<T>(this IEnumerable<T> values, params Expression<Func<T, object>>[] valueSelectors)
        {
            Console.WriteLine(values.ToStringTable(valueSelectors));
        }

        private static PropertyInfo GetProperty<T>(Expression<Func<T, object>> expresstion)
        {
            if (expresstion.Body is UnaryExpression)
            {
                if ((expresstion.Body as UnaryExpression).Operand is MemberExpression)
                {
                    return ((expresstion.Body as UnaryExpression).Operand as MemberExpression).Member as PropertyInfo;
                }
            }

            if ((expresstion.Body is MemberExpression))
            {
                return (expresstion.Body as MemberExpression).Member as PropertyInfo;
            }
            return null;
        }
    }
}
