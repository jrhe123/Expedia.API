### DotNet Core 7.0

- https://learn.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-7.0


### Configuration (env variables / ports / dev server)

- Properties/launchSettings.json


### docker
- reference: https://learn.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver16&pivots=cs1-bash
- docker pull mcr.microsoft.com/azure-sql-edge
- docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=PgPm0tUXb2jY33cPTv3a' \
    -p 1433:1433 -d mcr.microsoft.com/azure-sql-edge

- DBeaver (SQL server)
- user: sa
- password: PgPm0tUXb2jY33cPTv3a


### Manage NuGet Packages
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Newtonsoft.Json


### Migrate db in terminal
* install dotnet tool
cmd: dotnet tool install --global dotnet-ef

- cd /yourproject (/Users/jiaronghe/Desktop/projects/dotnet/Expedia.API/Expedia.API)

* generate migration files
- dotnet ef migrations add initialMigration
- dotnet ef migrations add dataSeeding
- dotnet ef migrations add updateTouristRouteSchema

* cancel
- dotnet ef migrations remove

* execute
- dotnet ef database update