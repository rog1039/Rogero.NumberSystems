using System;
using System.ComponentModel;
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

        public char FirstSymbol => Alphabet[0];
        public char LastSymbol => Alphabet[Alphabet.Length-1];


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
}
