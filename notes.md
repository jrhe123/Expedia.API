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

### mysql (optional)
- docker pull mysql:latest
- docker run -itd --name mysql-test -p 3306:3306 \
    -e MYSQL_ROOT_PASSWORD=123456 mysql:latest

### Postman
- https://api.postman.com/collections/2637368-d5ee22c1-0e6b-445a-b863-b3d279eed3ee?access_key=
- 200 OK, 201 Created, 204 No Content
- 301/302 Moved Permanently, 304 Not Modified
- 400 Bad Request, 401 Unauthorized, 403 Forbidden, 404 Not Found
- 500 Internal Server Error, 502 Bad Gateway

### Rest
- Headers
    - Accept
        - application/xml
        - application/json
    - Content-Type

### Manage NuGet Packages
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Newtonsoft.Json -> parse json string, deserialized
- Pomelo.EntityFrameworkCore.MySql
- AutoMapper.Extensions.Microsoft.DependencyInjection -> dto mapping


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