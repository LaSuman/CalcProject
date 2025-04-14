namespace CalculatorProject.Models
{
    public class Operation
    {
        public string? ID { get; set; }

        public List<string>? Value { get; set; }

        public Operation? NestedOperation { get; set; }

    }
}
