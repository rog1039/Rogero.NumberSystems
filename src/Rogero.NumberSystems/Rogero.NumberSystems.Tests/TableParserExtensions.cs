using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Rogero.NumberSystems.Tests;

public static class TableParserExtensions
{
    public static void PrintStringTable<T>(this IEnumerable<T> values)
    {
        Console.WriteLine(values.ToStringTable());
    }
    public static string ToStringTable<T>(this IEnumerable<T> values)
    {
        var objectProperties = GetObjectProperties<T>(values);
        var columnHeaders    = new string[objectProperties.Length];
        var valueSelectors   = new Func<T, object>[objectProperties.Length];

        for (int i = 0; i < objectProperties.Count(); i++)
        {
            var propertyInfo  = objectProperties[i];
            var columnHeader  = propertyInfo.Name;
            var valueSelector = new Func<T, object>(input => propertyInfo.GetValue(input, null));

            columnHeaders[i]  = columnHeader;
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
            var objectProperties = typeof(T).GetProperties();
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
        var   headerSpliter   = new string('-', maxColumnsWidth.Sum(i => i + 3) - 1);

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
        var headers   = valueSelectors.Select(func => GetProperty(func).Name).ToArray();
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