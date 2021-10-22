# DsRule
**DsRule** stands for Domain-specific Rule. It used to parse Domain-specific language to Linq Expression.

## Install


## Example
* Model
	```csharp
	var model = new Employee { FirstName = "Vincent", LastName = "Any", Age = 30 };
	```
* Domain-specific Expression
	```csharp
	var dsExpr = "FirstName = 'Vincent' AND Age > 25";
	```
* Execution Result
	```csharp
	var result = DsRuleExecutor.Execute<Employee, bool>(model, dsExpr);
	// 'result' is true
	```

For more examples, please refer to 
