
# Recursive Calculator Web API

A C# Web API that performs mathematical calculations using recursive operations defined via JSON or XML input. The service supports addition, subtraction, multiplication, division, and exponentiation with nested operations.

---

## ✨ Features

- ✅ Supports both JSON and XML input
- ✅ Recursive nested operations
- ✅ Handles constants like `e`
- ✅ Implements SOLID principles via service classes
- ✅ Logging with `ILogger<T>`
- ✅ xUnit test coverage for all operations and edge cases
- ✅ Custom JSON converter to handle flexible structures

---

## 🧩 Supported Operations

| Operator       | Description                  |
|----------------|------------------------------|
| Plus           | Addition of numbers           |
| Subtraction    | Subtraction from left to right|
| Multiplication | Product of values             |
| Division       | Sequential division with checks|
| Exponential    | `base^exponent` (supports `e`) |

---

## 📦 Input Format

### ✅ JSON

```json
{
  "MyMaths": {
    "MyOperation": {
      "@ID": "Plus",
      "Value": ["2", "3"],
      "MyOperation": {
        "@ID": "Multiplication",
        "Value": ["4"],
        "MyOperation": {
          "@ID": "Exponential",
          "Value": ["e", "2"]
        }
      }
    }
  }
}
```

### ✅ XML

```xml
<Maths>
  <Operation ID="Plus">
    <Value>2</Value>
    <Value>3</Value>
    <Operation ID="Multiplication">
      <Value>4</Value>
      <Operation ID="Exponential">
        <Value>e</Value>
        <Value>2</Value>
      </Operation>
    </Operation>
  </Operation>
</Maths>
```

---

## 🧭 Architecture Overview

```
[Client] --> [Controller] --> [IOperationService]
                         └--> AddService / SubService / MulService / ...
                                      └--> Recursively calculate Operation
```

---

## 🚀 Running the API

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Visual Studio or VS Code

### Start the API

```bash
dotnet build
dotnet run
```

The API will be available at `https://localhost:5001`

---

## 🔧 Endpoints

| Method | Route            | Content-Type       | Description              |
|--------|------------------|--------------------|--------------------------|
| POST   | /CalculateJSON   | application/json   | Calculates JSON input    |
| POST   | /CalculateXML    | application/xml    | Calculates XML input     |

---

## 🧪 Sample xUnit Test

```csharp
[Fact(DisplayName = "Should add values and nested division")]
public void AddService_ShouldCalculateRecursivePlusWithDivision()
{
    var logger = Mock.Of<ILogger<AddService>>();
    var service = new AddService(logger);

    var request = new CalculatorRequest
    {
        Maths = new Maths
        {
            Operation = new Operation
            {
                ID = "Plus",
                Value = new List<string> { "2", "3" },
                NestedOperation = new List<Operation>
                {
                    new Operation
                    {
                        ID = "Division",
                        Value = new List<string> { "4", "5" }
                    }
                }
            }
        }
    };

    double result = service.Calculate(request);
    Assert.Equal(5.8, result, 1); // 2 + 3 + (4 / 5)
}
```

---

## 🙋 Internal Contribution Guidelines

This is a public repository maintained by the internal development team. While the code is visible for transparency and collaboration, external contributions are not currently accepted.

### For internal developers:

1. **Clone the repository**  
2. **Create a feature branch**:  
   ```bash
   git checkout -b feature/your-feature
   ```

3. **Follow naming conventions** and maintain consistency with existing code  
4. **Write unit tests** for any new functionality  
5. Run:
   ```bash
   dotnet test
   ```
   Ensure all tests pass before pushing

6. **Push your branch** and open a pull request for review

> ⚠️ External pull requests will not be merged at this time.

---

## 🛠 Project Structure

```
CalculatorProject/
├── Controllers/
│   └── CalculatorController.cs
├── Models/
│   └── Operation.cs, CalculatorRequest.cs, ...
├── Services/
│   └── AddService.cs, SubService.cs, ...
├── Middleware/
│   └── XmlTagMappingMiddleware.cs
├── Tests/
│   └── MulServiceTest.cs, AddServiceTest.cs, ...
└── Program.cs
```

---

## 📄 License

This project is licensed under the MIT License.
