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
            if (xmlOperation == null)
                return null;
            return new Operation{

            ID = xmlOperation.ID,
                    Value = xmlOperation.Value,
                NestedOperation = xmlOperation.NestedOperation?
                    .Select(ConvertOperation)
                    .Where(op => op != null)
                    .ToList()
            };
        }
    }
}
