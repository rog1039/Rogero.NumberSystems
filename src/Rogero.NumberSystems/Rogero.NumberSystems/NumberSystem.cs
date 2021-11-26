namespace Rogero.NumberSystems;

public class NumberSystem
{
    public NumberAlphabet  NumberAlphabet  { get; }
    public string          Description     { get; }
    public FirstSymbolType FirstSymbolType { get; }
    public int             Radix           => NumberAlphabet.Alphabet.Length;

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
        NumberAlphabet       = numberAlphabet;
        FirstSymbolType      = firstSymbolType;
        FirstSymbolCollapses = firstSymbolCollapses;
        Description          = description;
    }

    public NumberSystem(NumberAlphabet numberAlphabet, string description)
    {
        NumberAlphabet = numberAlphabet;
        Description    = description;
    }


    public int ToOrdinal(char value)
    {
        var ordinal = NumberAlphabet.ToOrdinal(value);
        return FirstSymbolType == FirstSymbolType.Zero
            ? ordinal
            : ordinal + 1;
    }

    public char ToSymbol(int ordinal)
    {
        ordinal = FirstSymbolType == FirstSymbolType.Zero
            ? ordinal
            : ordinal - 1;
        return NumberAlphabet.Alphabet[ordinal];
    }

    public static NumberSystem Binary => new NumberSystem(NumberAlphabet.Binary, FirstSymbolType.Zero, true, "Binary-Collapse");
    public static NumberSystem BinaryNoCollapse => new NumberSystem(NumberAlphabet.Binary, FirstSymbolType.Zero, false, "Binary-NoCollapse");
    public static NumberSystem Trinary => new NumberSystem(NumberAlphabet.Trinary, FirstSymbolType.Zero, true, "Trinary-Collapse");
    public static NumberSystem TrinaryNoCollapse => new NumberSystem(NumberAlphabet.Trinary, FirstSymbolType.Zero, false, "Trinary-NoCollapse");
    public static NumberSystem Octal => new NumberSystem(NumberAlphabet.Octal, FirstSymbolType.Zero, true, "Octal-Collapse");
    public static NumberSystem Decimal => new NumberSystem(NumberAlphabet.Decimal, FirstSymbolType.Zero, true, "Decimal-Collapse");
    public static NumberSystem Hexadecimal => new NumberSystem(NumberAlphabet.Hexadecimal, FirstSymbolType.Zero, true, "Hexadecimal-Collapse");
    public static NumberSystem AToZ_ZeroIndex => new NumberSystem(NumberAlphabet.AToZ, FirstSymbolType.Zero, false, "A-Z=ZeroBased");
    public static NumberSystem AToZ_OneIndex => new NumberSystem(NumberAlphabet.AToZ, FirstSymbolType.One, false, "A-Z=OneBased");
    public static NumberSystem ZeroToZ_ZeroIndex => new NumberSystem(NumberAlphabet.ZeroToZ, FirstSymbolType.Zero, false, "0-Z=ZeroBased");
    public static NumberSystem ZeroToZ_OneIndex => new NumberSystem(NumberAlphabet.ZeroToZ, FirstSymbolType.One, false, "0-Z=OneBased");
    public static NumberSystem AToA_ZeroIndex => new NumberSystem(NumberAlphabet.AToA, FirstSymbolType.Zero, false, "A-A=ZeroBased");
    public static NumberSystem AToA_OneIndex => new NumberSystem(NumberAlphabet.AToA, FirstSymbolType.One, false, "A-A=OneBased");
    public static NumberSystem AToB_ZeroIndex => new NumberSystem(NumberAlphabet.AToB, FirstSymbolType.Zero, false, "A-B=ZeroBased");
    public static NumberSystem AToB_OneIndex => new NumberSystem(NumberAlphabet.AToB, FirstSymbolType.One, false, "A-B=OneBased");

    public override string ToString()
    {
        var collapseSymbol = FirstSymbolCollapses ? "C" : "N";
        var indexSymbol    = FirstSymbolType == FirstSymbolType.Zero ? "0" : "1";
        return $"{NumberAlphabet.FirstSymbol}-{NumberAlphabet.LastSymbol}|{collapseSymbol}{indexSymbol}";
    }

    public NumberSystem InvertFirstSymbolCollapses() => new NumberSystem(NumberAlphabet, FirstSymbolType, !FirstSymbolCollapses);
}