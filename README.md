# Difference-finder-api
Difference finder api

This API calculates the differences in two given base64-econded strings.

## Behaviours of API

The API enables to three endpoints to accomplish this task.

- If value of the "input" property of diffed JSONs is equal, returns the information “inputs were equal”. 
- If value of the "input" property of diffed JSONs is not of equal size, returns the information “inputs are of different size”. 
- If value of the "input" property of diffed JSONs has the same size, perform a simple diff.

### Constraints
  - The input **Id** is required and must be integer.
  - The input **Str** is required, not nullable and should be encoded with base64.

### Output structure

The results produced will be given in the output structure below.
```json
{
  "id": 0,
  "resultMessage": "string",
  "differences": [
    {
      "offset": 0,
      "diff": 0
    }
  ]
}
```

## Prequisites
 - ASP.NET Core 5 (or later)

### Usage
- Inserts to left
```console
    curl -X POST <host>/diff/<Id>/left -H  "accept: text/plain" -H  "Content-Type: application/json" -d "{\"data\":\"<Str>\"}"
```
- Inserts to right
```console
    curl -X POST <host>/diff/<Id>/left -H  "accept: text/plain" -H  "Content-Type: application/json" -d "{\"data\":\"<Str>\"}"
```
- To get results
```console
    curl -X GET "<host>/diff/<Id>" -H  "accept: text/plain"
```

## Some Improvment Suggestions
- A database application can be implemented to ensure data persistence. To keep the project simple, the variables were stored in the ```List```.

- In case of adding the entry item with the existing Id, the new incoming entry in the existing solution overwrites the existing one.
User confirmation can be added to avoid any faulty user action.

- The default log provider in the current application is Console. Providers can be changed if log history becomes important.

- The AOP paradigm can be followed to reduce repetitive log insertion codes.

