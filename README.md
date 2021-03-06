# DsRule
**DsRule** stands for Domain-specific Rule. 

## What is DsRule?
DsRule is used to parse domain-specific expression (in string) to Linq Expression and execute.

## Install
```
dotnet add package DsRule
```

## Usage
The simplest case is parsing below expression
```csharp
var result = DsRuleExecutor.Execute<int>("1 + 1");
// result is 2
```
A more complicated example for domain-specific expression
```csharp
var model = new Employee { FirstName = "Vincent", LastName = "Any", Age = 30 };
var dsExpr = "FirstName = 'Vincent' AND Age > 25";

var result = DsRuleExecutor.Execute<Employee, bool>(model, dsExpr);
// 'result' is true
```

You can also use expresion in Where caluse
```csharp
var employees = new List<Employee> { ... }; // A list of employees
var filtered = employees.Where("FirstName = 'Vincent' AND Age > 25");
```

For more examples, please refer to [Examples](https://github.com/vincent-scw/DsRule/blob/main/test/DsRule.UnitTest/DsRuleExecutorTests.cs)

## Domain-specific language grammar
|Kind|--|Description|
|---|---|---|
|Comparand|+, -, *, /||
|Comparison|<, >, <=, >=, =, !=, <>| |
|Keyword|not, and, or, true, false, null| |
|Keyword|in| Used for Array |
|Keyword|now, today| Will be parsed to DateTime.Now and DateTime.Today|
