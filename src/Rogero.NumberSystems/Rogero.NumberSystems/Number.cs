namespace Rogero.NumberSystems;

public class Number
{
    public NumberSystem NumberSystem { get; set; }
    public string       Value        { get; set; }

    public Number(NumberSystem numberSystem, string value)
    {
        NumberSystem = numberSystem;
        Value        = value;
    }

    public override string ToString()
    {
        return $"{Value,3} [{NumberSystem}]";
    }
}

public static class NumberExtensions
{
    public static int ToDecimalValue(this Number number)
    {
        var decimalNumber = number.NumberSystem == NumberSystem.Decimal
            ? number
            : NumberConverter.Convert(number, NumberSystem.Decimal);
        var stringRepresentation = decimalNumber.Value;
        var intValue             = int.Parse(stringRepresentation);
        return intValue;
    }

    public static Number ToDecimal(this Number number) => NumberConverter.Convert(number, NumberSystem.Decimal);

    public static Number ToNumber(this Number number, NumberSystem numberSystem) => NumberConverter.Convert(number, numberSystem);
}