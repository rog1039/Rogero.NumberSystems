namespace Rogero.NumberSystems
{
    public class Number
    {
        public NumberSystem NumberSystem { get; set; }
        public string Value { get; set; }

        public Number(NumberSystem numberSystem, string value)
        {
            NumberSystem = numberSystem;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Value,3} [{NumberSystem}]";
        }
    }
}