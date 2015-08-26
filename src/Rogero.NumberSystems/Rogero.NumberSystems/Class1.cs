﻿using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Means the first character in the alphabet is a special 'zero' character.
        /// 
        /// Exists so we can handle these two cases differently:
        /// Decimal: 7, 8, 9, 10 -> then 11, not 01 - HasZeroCharacter = true;
        /// A to Z : W, X, Y, Z  -> then AA, not BA - HasZeroCharacter = false;
        /// </summary>
        public bool HasZeroCharacter { get; set; }

        public NumberAlphabet(char[] alphabet, bool hasZeroCharacter)
        {
            Alphabet = alphabet;
            HasZeroCharacter = hasZeroCharacter;
        }

        public NumberAlphabet(string alphabet, bool hasZeroCharacter)
        {
            Alphabet = alphabet.ToCharArray();
            HasZeroCharacter = hasZeroCharacter;
        }

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
            if (!HasZeroCharacter) ordinal--;
            return Alphabet[ordinal];
        }

        #region Static Alphabets
        public static NumberAlphabet Binary => _binary;
        private static readonly NumberAlphabet _binary = new NumberAlphabet("01", true);

        public static NumberAlphabet Octal => _octal;
        private static NumberAlphabet _octal = new NumberAlphabet("01234567", true);

        public static NumberAlphabet Decimal => _decimal;
        private static readonly NumberAlphabet _decimal = new NumberAlphabet("0123456789", true);

        public static NumberAlphabet Hexadecimal => _hexadecimal;
        private static NumberAlphabet _hexadecimal = new NumberAlphabet("0123456789ABCDEF", true);

        public static NumberAlphabet AToZ => _aToZ;
        static readonly NumberAlphabet _aToZ = new NumberAlphabet("ABCDEFGHIJKLMNOPQRSTUVWXYZ", false);

        public static NumberAlphabet ZeroToZ => _zeroToZ;
        private static readonly NumberAlphabet _zeroToZ = new NumberAlphabet("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", true);

        public static NumberAlphabet AToA => _aToA;
        private static readonly NumberAlphabet _aToA = new NumberAlphabet("A", false);
        #endregion
    }

    public class NumberSystem
    {
        public NumberAlphabet NumberAlphabet { get; set; }
        public int Radix => NumberAlphabet.Alphabet.Length;
        public string Description { get; set; }

        public NumberSystem(NumberAlphabet numberAlphabet)
        {
            NumberAlphabet = numberAlphabet;
        }

        public NumberSystem(NumberAlphabet numberAlphabet, string description)
        {
            NumberAlphabet = numberAlphabet;
            Description = description;
        }

        public static NumberSystem Binary => new NumberSystem(NumberAlphabet.Binary, "Binary");
        public static NumberSystem Octal => new NumberSystem(NumberAlphabet.Octal, "Octal");
        public static NumberSystem Decimal => new NumberSystem(NumberAlphabet.Decimal, "Decimal");
        public static NumberSystem Hexadecimal => new NumberSystem(NumberAlphabet.Hexadecimal, "Hexadecimal");
        public static NumberSystem AToZ => new NumberSystem(NumberAlphabet.AToZ, "AToZ");
        public static NumberSystem ZeroToZ => new NumberSystem(NumberAlphabet.ZeroToZ, "0ToZ");
        public static NumberSystem AToA => new NumberSystem(NumberAlphabet.AToA, "AToA");

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
                if (radix == 1) divisionResult--;
            }
            while (divisionResult >= outputNumberSystem.Radix);
            if (divisionResult > 0)
            {
                if (!outputNumberSystem.NumberAlphabet.HasZeroCharacter)
                {
                    divisionResult--;
                }
                result.Add(divisionResult);
            }

            var symbolList = result.Select((value, index) => outputNumberSystem.NumberAlphabet.ToSymbol(value)).ToList();
            symbolList.Reverse();
            var resultString = symbolList.Aggregate(string.Empty, (s, c) => s += c);
            return new Number(outputNumberSystem, resultString);
        }
    }
}