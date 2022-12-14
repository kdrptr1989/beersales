# BeerSale project
Demo project for Brewery and Wholesaler management

You can find the specifications here: 
- https://github.com/kdrptr1989/beersales/blob/master/Test%20Specifications%20-%20Brewery%20and%20wholesale%20management.pdf

#Technology stack
- .NET 6 - Minimal api
    - Record types for Domain objects and Data Transfer Objects
    - Built-in ILogger
- CQRS design pattern with MediatR
- Apply CleanArchitecture solution with Api, Core, Domain, Infrastructure, tests projects 
- Repositories with CRUD operations
- FluentValidation
- SwaggerUI
- EntityFramework Core
   - DbMigration with generated script

Unit tests
- XUnit
- NSubstitute
 
Dockerfile & Docker-compose
   - Launch SQL Server in docker compose, image: mcr.microsoft.com/mssql/server:2019-latest

Future plan:
- Use Automapper
- Setup CI/CD with ARM templates and free Azure account

Note and have to fix:
- Docker can't reach the localdb on "localhost" or "127.0.0.1" I had to set the direct ip address in appsettings.Development.json
- If you try to launch at localhost you have to modify the appsettings with your local ip address
