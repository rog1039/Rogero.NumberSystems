using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogero.NumberSystems
{
    public class NumberAlphabet
    {
        /// <summary>
        /// The symbols in the number system alphabet. This is in the order from lowest to highest.
        /// </summary>
        public char[] Alphabet { get; }
        
        public NumberAlphabet(char[] alphabet)
        {
            Alphabet = alphabet;
        }

        public NumberAlphabet(string alphabet) : this(alphabet.ToCharArray()) { }

        public int ToOrdinal(char value)
        {
            for (int i = 0; i < Alphabet.Length; i++)
            {
                if (value == Alphabet[i])
                {
                    return i;
                }
            }
            throw new ArgumentException($"'{value}' is not present in the Alphabet, {new string(Alphabet)}");
        }

        public char ToSymbol(int ordinal)
        {
            return Alphabet[ordinal];
        }

        #region Static Alphabets
        public static NumberAlphabet Binary => _binary;
        private static readonly NumberAlphabet _binary = new NumberAlphabet("01");
        public static NumberAlphabet Trinary => _trinary;
        private static readonly NumberAlphabet _trinary = new NumberAlphabet("012");

        public static NumberAlphabet Octal => _octal;
        private static NumberAlphabet _octal = new NumberAlphabet("01234567");

        public static NumberAlphabet Decimal => _decimal;
        private static readonly NumberAlphabet _decimal = new NumberAlphabet("0123456789");

        public static NumberAlphabet Hexadecimal => _hexadecimal;
        private static NumberAlphabet _hexadecimal = new NumberAlphabet("0123456789ABCDEF");

        public static NumberAlphabet AToZ => _aToZ;
        static readonly NumberAlphabet _aToZ = new NumberAlphabet("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

        public static NumberAlphabet ZeroToZ => _zeroToZ;
        private static readonly NumberAlphabet _zeroToZ = new NumberAlphabet("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");

        public static NumberAlphabet AToA => _aToA;
        private static readonly NumberAlphabet _aToA = new NumberAlphabet("A");

        public static NumberAlphabet AToB => _aToB;
        private static readonly NumberAlphabet _aToB = new NumberAlphabet("AB");
        #endregion
    }

    /// <summary>
    /// Does the first symbol get translated to a value of zero or 1?
    /// </summary>
    public enum FirstSymbolType
    {
        Zero,
        One
    }

    public class NumberSystem
    {
        public NumberAlphabet NumberAlphabet { get; }
        public string Description { get; }
        public FirstSymbolType FirstSymbolType { get; }
        public int Radix => NumberAlphabet.Alphabet.Length;

        /// <summary>
        /// Means the first character in the alphabet is a special 'zero' character.
        /// 
        /// Exists so we can handle these two cases differently:
        /// Decimal: 7, 8, 9, 10 -> then 11, not 01 - ZeroCollapses = true; 
        /// A to Z : W, X, Y, Z  -> then AA, not BA - ZeroCollapses = false;
        /// 
        /// This is because 00 collapses to 0, a char we've seen already (00 == 0). However,
        /// AA does not collapse to A since it stands for a unique value from A (AA != A).
        /// </summary>
        public bool FirstSymbolCollapses { get; set; }

        public NumberSystem(NumberAlphabet numberAlphabet, FirstSymbolType firstSymbolType, bool firstSymbolCollapses, string description = default(string))
        {
            NumberAlphabet = numberAlphabet;
            FirstSymbolType = firstSymbolType;
            FirstSymbolCollapses = firstSymbolCollapses;
            Description = description;
        }

        public NumberSystem(NumberAlphabet numberAlphabet, string description)
        {
            NumberAlphabet = numberAlphabet;
            Description = description;
        }


        public int ToOrdinal(char value)
        {
            for (int i = 0; i < NumberAlphabet.Alphabet.Length; i++)
            {
                if (value == NumberAlphabet.Alphabet[i])
                {
                    return FirstSymbolType == FirstSymbolType.Zero
                        ? i
                        : i + 1;
                }
            }
            throw new ArgumentException($"'{value}' is not present in the Alphabet, {new string(NumberAlphabet.Alphabet)}");
        }

        public char ToSymbol(int ordinal)
        {
            ordinal = FirstSymbolType == FirstSymbolType.Zero
                ? ordinal
                : ordinal - 1;
            return NumberAlphabet.Alphabet[ordinal];
        }

        public static NumberSystem Binary => new NumberSystem(NumberAlphabet.Binary, FirstSymbolType.Zero, true, "Binary");
        public static NumberSystem BinaryNoCollapse => new NumberSystem(NumberAlphabet.Binary, FirstSymbolType.Zero, false, "Binary-NoCollapse");
        public static NumberSystem Trinary => new NumberSystem(NumberAlphabet.Trinary, FirstSymbolType.Zero, true, "Trinary");
        public static NumberSystem TrinaryNoCollapse => new NumberSystem(NumberAlphabet.Trinary, FirstSymbolType.Zero, false, "Trinary-NoCollapse");
        public static NumberSystem Octal => new NumberSystem(NumberAlphabet.Octal, FirstSymbolType.Zero, true, "Octal");
        public static NumberSystem Decimal => new NumberSystem(NumberAlphabet.Decimal, FirstSymbolType.Zero, true, "Decimal");
        public static NumberSystem Hexadecimal => new NumberSystem(NumberAlphabet.Hexadecimal, FirstSymbolType.Zero, true, "Hexadecimal");
        public static NumberSystem AToZ_ZeroIndex => new NumberSystem(NumberAlphabet.AToZ, FirstSymbolType.Zero, false, "AToZ-ZeroBased");
        public static NumberSystem AToZ_OneIndex => new NumberSystem(NumberAlphabet.AToZ, FirstSymbolType.One, false, "AToZ-OneBased");
        public static NumberSystem ZeroToZ_ZeroIndex => new NumberSystem(NumberAlphabet.ZeroToZ, FirstSymbolType.Zero, false, "0ToZ-ZeroBased");
        public static NumberSystem ZeroToZ_OneIndex => new NumberSystem(NumberAlphabet.ZeroToZ, FirstSymbolType.One, false, "0ToZ-OneBased");
        public static NumberSystem AToA_ZeroIndex => new NumberSystem(NumberAlphabet.AToA, FirstSymbolType.Zero, false, "AToA-ZeroBased");
        public static NumberSystem AToA_OneIndex => new NumberSystem(NumberAlphabet.AToA, FirstSymbolType.One, false, "AToA-OneBased");
        public static NumberSystem AToB_ZeroIndex => new NumberSystem(NumberAlphabet.AToB, FirstSymbolType.Zero, false, "AToB-ZeroBased");
        public static NumberSystem AToB_OneIndex => new NumberSystem(NumberAlphabet.AToB, FirstSymbolType.One, false, "AToB-OneBased");

        public override string ToString()
        {
            return Description;
        }
    }

    public static class Converter
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
            return ConvertDecimalToNumberSystem(inputValue, outputNumberSystem);
        }

        private static int ConvertNumberToDecimal(string inputValue, NumberSystem inputNumberSystem)
        {
            var reversedInputValue = inputValue.Reverse();
            int decimalValue = reversedInputValue
                .Select((ch, index) => GetDecimalValueOfCharacter(inputNumberSystem, index, ch))
                .Sum();
            return decimalValue;
        }

        private static int GetDecimalValueOfCharacter(NumberSystem inputNumberSystem, int index, char ch)
        {
            var ordinalOfNumber = inputNumberSystem.NumberAlphabet.ToOrdinal(ch);
            var multiplicationFactor = (int)Math.Pow(inputNumberSystem.Radix, index);
            ordinalOfNumber = index == 0 ? ordinalOfNumber : ordinalOfNumber + 1;
            return multiplicationFactor * ordinalOfNumber;
        }

        private static Number ConvertDecimalToNumberSystem(int decimalValue, NumberSystem outputNumberSystem)
        {
            return outputNumberSystem.FirstSymbolCollapses
                ? ConvertDecimalToCollapsableNumberSystem(decimalValue, outputNumberSystem)
                : ConvertDecimalToNonCollapsableNumberSystem(decimalValue, outputNumberSystem);
        }

        private static Number ConvertDecimalToNonCollapsableNumberSystem(int decimalValue, NumberSystem outputNumberSystem)
        {
            int radix = outputNumberSystem.Radix;
            int quotient = 0, mod = 0, value = decimalValue, iteration = 1;
            double valuesAtThisLevel = 0;
            int sumOfPowers = 0;
            while (true)
            {
                var v = (int)Math.Pow(radix, iteration);
                var newSumOfPowers = sumOfPowers + v;
                if (decimalValue < newSumOfPowers) break;
                sumOfPowers = newSumOfPowers;
                iteration++;
            }
            var z = iteration;
            var v2 = decimalValue - sumOfPowers;
            var newNumber = ConvertDecimalToCollapsableNumberSystem(v2, outputNumberSystem);
            var paddedNumber = PadWithFirstSymbol(newNumber, iteration, outputNumberSystem);

            return paddedNumber;

            var log2 = Math.Floor(Math.Log(decimalValue, radix));
            int log3 = log2 < 0 ? 0 : (int)log2;
            var log = 1;
            //var log = (int)Math.Floor(Math.Log(decimalValue, radix));
            var sum = Enumerable.Range(1, log3).Select(y => Math.Pow(radix, y)).Sum();
            var toSubtract = log == 0 ? 0 : Math.Pow(radix, log);
            var remainder = (int) (decimalValue - sum);

            var widthOfOutputNumber = log + 1;
            var newNumber2 = ConvertDecimalToCollapsableNumberSystem(remainder, outputNumberSystem);

            return newNumber;
        }

        private static Number PadWithFirstSymbol(Number newNumber, int iteration, NumberSystem outputNumberSystem)
        {
            var currentLength = newNumber.Value.Length;
            var additionalCharsRequired = iteration - currentLength;
            var padding = new string(outputNumberSystem.NumberAlphabet.Alphabet[0], additionalCharsRequired);
            var newValue = padding + newNumber.Value;
            return new Number(outputNumberSystem, newValue);
        }

        private static Number ConvertDecimalToCollapsableNumberSystem(int decimalValue, NumberSystem outputNumberSystem)
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


        private static Number ConvertDecimalToNonZeroBasedNumberSystem(int decimalValue, NumberSystem outputNumberSystem)
        {
            CheckNotZero(decimalValue);
            int quotient = 0;
            int mod = 0;
            int value = decimalValue;
            int radix = outputNumberSystem.Radix;
            int valueToPush = 0;
            int valueToSubtract = 0;
            var stack = new Stack<int>();

            while (value > 0)
            {
                mod = value%radix;
                quotient = value/radix;
                valueToSubtract = mod == 0 ? radix : mod;
                value -= valueToSubtract;
                stack.Push(mod);
            }
            var symbols = stack.ToList().Select(val => val++).ToList();
            Console.WriteLine(symbols.ToArray().Aggregate("", (s,i) => s+=i));

            return new Number(outputNumberSystem, "1");
        }

        private static void CheckNotZero(int decimalValue)
        {
            if (decimalValue == 0)
                throw new ArgumentException("decimalValue cannot be zero in a non-zero based number system.");
        }

        private static Number ConvertDecimalToNonZeroBasedNumberSystem2(int decimalValue, NumberSystem outputNumberSystem)
        {
            if (decimalValue == 0)
                throw new ArgumentException("decimalValue cannot be zero in a non-zero based number system.");

            var result = new List<int>();
            var radix = outputNumberSystem.Radix;

            int number = decimalValue - 1;
            int divisionResult = 0;
            int moduloResult = 0;

            bool firstIteration = true;
            do
            {
                divisionResult = number/radix;
                moduloResult = number%radix;
                var resultToAdd = moduloResult == 0 ? 1 : (firstIteration ? moduloResult + 1 : moduloResult);
                result.Add(resultToAdd);
                number = divisionResult;
                firstIteration = false;
            } while (divisionResult > 0);

            result = result.ToList();
            var newSymbols = result.Select(value => outputNumberSystem.ToSymbol(value)).ToList();
            newSymbols.Reverse();
            var resultString2 = newSymbols.Aggregate("", (s, c) => s += c);
            return new Number(outputNumberSystem, resultString2);





            //do
            //{
            //    divisionResult = number / radix;
            //    moduloResult = number % radix;
            //    result.Add(moduloResult);
            //    number = divisionResult;
            //    if (radix == 1) number--;
            //}
            //while (divisionResult >= outputNumberSystem.Radix);
            //if (divisionResult > 0)
            //{
            //    result.Add(divisionResult);
            //}
            //var newResult = result;

            //var symbolList = result.Select((value, index) => outputNumberSystem.NumberAlphabet.ToSymbol(value)).ToList();
            //symbolList.Reverse();
            //var resultString = symbolList.Aggregate(string.Empty, (s, c) => s += c);
            //return new Number(outputNumberSystem, resultString);
        }
    }

    public static class NonZeroBasedNumberSystemConverter
    {
        
    }
}
