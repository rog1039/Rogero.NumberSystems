using System;
using System.Collections.Generic;
using System.Linq;

namespace Rogero.NumberSystems
{
    public static class NonCollapsibleToDecimalConverter
    {
        public static int Convert(Number number)
        {
            if(number.NumberSystem.FirstSymbolCollapses)
                throw new InvalidOperationException("This converter is for numbers that are not collapsible.");

            int valueAsCollapsible = GetDecimalValueAsCollapsible(number);
            int nonCollapsiblePortion = GetNoncollapsiblePortion(number);

            var value = valueAsCollapsible + nonCollapsiblePortion;

            if (number.NumberSystem.FirstSymbolType == FirstSymbolType.One)
                value++;

            return value;
        }

        private static int GetNoncollapsiblePortion(Number number)
        {
            int raiseToPower = number.Value.Length-1;
            if (raiseToPower == 0) return 0;

            var radix = number.NumberSystem.Radix;
            var value = 0;
            for (int i = 1; i <= raiseToPower; i++)
            {
                value += (int)Math.Pow(radix, i);
            }
            return value;
        }

        private static int GetDecimalValueAsCollapsible(Number number)
        {
            var numberAsCollapsible = new Number(number.NumberSystem.InvertFirstSymbolCollapses(), number.Value);
            var decimalValue = numberAsCollapsible.ToDecimalValue();
            return decimalValue;
        }
    }

    public static class NumberConverter
    {
        public static Number Convert(Number input, NumberSystem outputNumberSystem)
        {
            return Convert(input.Value, input.NumberSystem, outputNumberSystem);
        }

        public static Number Convert(string inputValue, NumberSystem inputNumberSystem, NumberSystem outputNumberSystem)
        {
            var decimalValue = ConvertNumberToDecimal(inputValue, inputNumberSystem);
            var result = ConvertDecimalToNumberSystem(decimalValue, outputNumberSystem);
            return result;
        }

        public static Number ConvertFromDecimal(int inputValue, NumberSystem outputNumberSystem)
        {
            if(outputNumberSystem.FirstSymbolType == FirstSymbolType.One)
                CheckNotZero(inputValue);
            return ConvertDecimalToNumberSystem(inputValue, outputNumberSystem);
        }
        private static void CheckNotZero(int decimalValue)
        {
            if (decimalValue == 0)
                throw new ArgumentException("decimalValue cannot be zero in a non-zero based number system.");
        }
        
        private static int ConvertNumberToDecimal(string inputValue, NumberSystem inputNumberSystem)
        {
            if (inputNumberSystem.FirstSymbolCollapses)
            {
                var reversedInputValue = inputValue.Reverse();
                int decimalValue = reversedInputValue
                    .Select((ch, index) => GetDecimalValueOfCharacter(inputNumberSystem, index, ch))
                    .Sum();
                return decimalValue;
            }
            else
            {
                var result = NonCollapsibleToDecimalConverter.Convert(new Number(inputNumberSystem, inputValue));
                return result;
            }
        }

        private static int GetDecimalValueOfCharacter(NumberSystem inputNumberSystem, int index, char ch)
        {
            var radix = inputNumberSystem.Radix;
            var coefficient = inputNumberSystem.NumberAlphabet.ToOrdinal(ch);
            var value = coefficient*(int)Math.Pow(radix, index);
            return value;
        }

        private static Number ConvertDecimalToNumberSystem(int decimalValue, NumberSystem outputNumberSystem)
        {
            return outputNumberSystem.FirstSymbolCollapses
                ? ConvertDecimalToCollapsibleNumberSystem(decimalValue, outputNumberSystem)
                : ConvertDecimalToNonCollapsibleNumberSystem(decimalValue, outputNumberSystem);
        }

        private static Number ConvertDecimalToNonCollapsibleNumberSystem(int decimalValue, NumberSystem outputNumberSystem)
        {
            if (outputNumberSystem.FirstSymbolType == FirstSymbolType.One)
                decimalValue--;
            int radix = outputNumberSystem.Radix;
            int iteration = 1;
            int powerResult = 0, sumOfPowers = 0, newSumOfPowers = 0;
            
            while (true)
            {
                powerResult = (int)Math.Pow(radix, iteration);
                newSumOfPowers = sumOfPowers + powerResult;
                if (decimalValue < newSumOfPowers) break;
                sumOfPowers = newSumOfPowers;
                iteration++;
            }
            var equivalentSymbolInCollapsibleSystem = decimalValue - sumOfPowers;
            var newNumber = ConvertDecimalToCollapsibleNumberSystem(equivalentSymbolInCollapsibleSystem, outputNumberSystem);
            var paddedNumber = PadWithFirstSymbol(newNumber, iteration, outputNumberSystem);

            return paddedNumber;
        }

        private static Number PadWithFirstSymbol(Number number, int requiredLength, NumberSystem numberSystem)
        {
            var currentValue = number.Value;
            var currentLength = currentValue.Length;
            var additionalCharsRequired = requiredLength - currentLength;
            var paddingSymbol = numberSystem.NumberAlphabet.Alphabet[0];
            var padding = new string(paddingSymbol, additionalCharsRequired);
            var newValue = padding + currentValue;
            return new Number(numberSystem, newValue);
        }

        private static Number ConvertDecimalToCollapsibleNumberSystem(int decimalValue, NumberSystem outputNumberSystem)
        {
            var radix = outputNumberSystem.Radix;
            int number = decimalValue;
            int divisionResult = 0;
            int moduloResult = 0;
            var result = new List<int>();
            do
            {
                divisionResult = number / radix;
                moduloResult = number % radix;
                result.Add(moduloResult);
                number = divisionResult;
            }
            while (divisionResult >= outputNumberSystem.Radix);
            if (divisionResult > 0)
            {
                result.Add(divisionResult);
            }

            var symbolList = result.Select((value, index) => outputNumberSystem.NumberAlphabet.ToSymbol(value)).ToList();
            symbolList.Reverse();
            var resultString = symbolList.Aggregate(string.Empty, (s, c) => s += c);
            return new Number(outputNumberSystem, resultString);
        }
    }
}