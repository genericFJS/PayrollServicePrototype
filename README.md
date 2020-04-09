# PayrollServicePrototype

![.NET Framework 4.7.2](https://img.shields.io/badge/.NET_Framework-4.7.2-green) ![WebAPI-2](https://img.shields.io/badge/WebAPI-2-green)  ![JSON endpoint](https://img.shields.io/badge/Endpoint-JSON-green) ![MSTests](https://img.shields.io/badge/MSTests-✔-green) ![DI Container](https://img.shields.io/badge/DI_Container-✔-green)

A small experimental prototype for a web API for calculating the net salary via hours worked and an hourly rate and deducting flat and progressiv taxes. 

## Examples

Endpoint ``/api/payrollservice/DEU`` returns

```json
[
    {
        "countryCode":"DEU",
        "grossSalary":200.0,
         "taxesDeductions":54.00,
         "netSalary":146.00
    },
    {
         "countryCode":"DEU",
         "grossSalary":1050.0,
         "taxesDeductions":329.00,
         "netSalary":721.00
    }
]
```

for the country Germany with an example flat tax of 2% and a progressive tax of 25% until 400€ which increases to 32%. The two example cases are provided by the service.

Another endpoint ``/api/payrollservice/DEU?hoursWorked=35&hourlyRate=30`` manually provides the example case with hours worked and an hourly rate and therefore returns

```json
{
    "countryCode":"DEU",
    "grossSalary":1050.0,
    "taxesDeductions":329.00,
    "netSalary":721.00
}
```