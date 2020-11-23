# MyBank
This solution uses .NET Core 3.1

## Set up database
This solution is using Microsoft SQL Server, you can use docker as:

```
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=yourStrong(!)Password' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest
```

and create database **mybank** and create tables using file **MyBank.sql**

## How to run backend
Before running the backend, please configure the connectionString at **appsettings.json** according to the database above.
```
cd backend/backend
dotnet run
```

and import **MyBank.postman_collection.json** to Postman to test the APIs.
Now the  APIs are working only happy cases.

## How to run backend tests
These tests are functinal tests to verify logic.
```
cd backend/backend.functionalTests/
dotnet test
```



## How to run froend
The fronend is not connect to the backend yet, it is just a set up project.
```
cd frontend/
yarn
yarn dev
```
