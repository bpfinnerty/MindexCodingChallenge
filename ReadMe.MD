# Mindex Coding Challenge

## How to Run
You can run this by executing `dotnet run` on the command line or in [Visual Studio Community Edition](https://www.visualstudio.com/downloads/).

This can be done in the .\CodeChallenge directory.

The easiest way to perform tests would be by running them through an IDE like [Visual Studio Community Edition](https://www.visualstudio.com/downloads/).
When you pull up a test there will be options to run test, debug test, etc. Select `run test`. 
This can also be done through extensions like .NET Core Test Explorer. 

There is an issue with using `dotnet test` as it is remembering previous states from other tests rather than isolating each one.


## Task 1
### How to Use

For reporting structure following JSON schema is used: 
```json
{
  "type":"ReportingStructure",
  "properties": {
    "employee": {
      "type": "string"
    },
    "numberOfReports": {
          "type": "int"
    },
  }
}
```

The following endpoints are available to use:
```
* READ
    * HTTP Method: GET 
    * URL: localhost:8080/api/reportingStructure/{id}
    * RESPONSE: ReportingStructure
```
### Further details
Because Reporting Structures were computed on the fly there is no data table for them in the database. Instead, when a get request is received, the 
`id` is parsed from the request and the employee table is queried. If an employee is found then the entire directReports structure of that employee
is generated. From there a method in the employee model is leveraged to traverse the direct reports structure and sum all reporters. 

A reporting structure object is then constructed of the fully filled out employee and the number of reporters it has. That is then
sent back to the client. Clarifying questions would have been used to determine desired behavior for displaying the employee. 

## Task 2
### How to Use

For compensation following JSON schema is used: 
```json
{
  "type":"Compensation",
  "properties": {
    "compensationId": {
      "type": "string"
    },
    "salary": {
        "type": "int"
    },
    "effectiveDate": {
        "type": "string"
    },
  }
}
```

The following endpoints are available to use:
```
* READ
    * HTTP Method: GET 
    * URL: localhost:8080/api/compensation/{id}
    * RESPONSE: Compensation
* Create
    * HTTP Method: Post 
    * URL: localhost:8080/api/compensation/{id}
    * RESPONSE: Compensation
    * Payload: Compensation
```

### Further details

A compensation table was created in the database to persist compensation entities throughout the lifecycle of the database. A compensation's compensationId was set to the related employeeId that the compensation entity referred to. This guaranteed that each compensation entity in the database would be unique and would be easily accessible. Another approach to accomplish this would be to retrieve the employee related to the compensation, add a new GUID as a foreign key to the compensation entity, set the GUID as the primary key of the compensation entity, and finally have the employeeId as a foreign key in the compensation entity. Questions would have been used to identify the desired approach. 

The employee entity is not directly stored into the compensation entity as it may not reflect any alterations made on the employee in the employee table. 

Another useful change would be to add salary and effectiveDate attributes to the employee rather than storing in a separate table. As this is a relatively simple system this could make sense but it depends on future needs (such as HR modifying salaries and when they take effect). In that example it would be better to allow for employees to have multiple compensations and store references to them as a list. Again, clarifying questions would have been used to discern desired behavior.