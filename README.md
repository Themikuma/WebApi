# WebApi
Proof of concept on a C# CRUD Web API.

## Running
To run the project make sure you have a local version of MSSQL running.
You can run the solution in Visual Studio and running with F5.

You can also run using command prompt and the following command.
Make sure you are in the web project directory WebApi/LHCRUD/LHCRUD/
```cmd
dotnet run
```

## Data

The project requires a single database named Bookstore. If the database is found, but no data exists data gets automatically seeded.
[LHCRUD.Data.Seeding.ApplicationDbSeeder](https://github.com/Themikuma/WebApi/blob/main/LHCRUD/LHCRUD.Data/Seeding/ApplicationDbSeeder.cs) The amount of data seeded can be changed by editing this file

## API Documentation

API documentation is fully done using SwaggerUI and XML comments on the methods.
