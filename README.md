# core-datatools
Core Data Tools, for searching pagination and filtering in LINQ/EF.

Data Tools uses dynamic Linq to allow dynamic queries to be executed via the SearchParameters object. 

## Example
```csharp
var parms = new SearchParameters
            {
                PageSize = 10,
                PageIndex = 0,
                SortOrder = SortOrder.Descending,
                SortFieldName ="Firstname",
                FilterExpression = "Lastname == @0",
                FilterParameters = new []{"Jefferson"}
                
            };
var query = context.People;

var result = parms.Apply<MyPeopleDto, MyPeopleEntity>(query);
```

The result will be a paginated list with a total, the items and the total number of pages. This library is useful in conjunction with the [Javascript Filter Expression Builder](https://github.com/ThoriumStack/js-filter-expression-builder)


