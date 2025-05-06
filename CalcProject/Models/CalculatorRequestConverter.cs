namespace CalculatorProject.Models
{
    public static class CalculatorRequestConverter
    {
        public static CalculatorRequest ToCalculatorRequest(this CalculatorRequestXml xml)
        {
            return xml == null
                ? throw new ArgumentNullException(nameof(xml))
                : new CalculatorRequest
                {
                    Maths = new Maths
                    {
                        Operation = ConvertOperation(xml.Operation)
                    }
                };
        }

        private static Operation? ConvertOperation(OperationXml? xmlOperation)
        {
            //Conditional Operation
            return xmlOperation == null
                ? null
                : new Operation
                {
                    ID = xmlOperation.ID,
                    Value = xmlOperation.Value,
                    NestedOperation = xmlOperation.NestedOperation != null && xmlOperation.NestedOperation.Any()
                    ? ConvertOperation(xmlOperation.NestedOperation.First()) // Fix: Convert the first nested operation instead of assigning a list
                    : null
                };
        }
    }
}
