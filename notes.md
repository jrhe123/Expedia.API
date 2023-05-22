### DotNet Core 7.0

- https://learn.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-7.0


### Configuration (env variables / ports / dev server)

- Properties/launchSettings.json

### HOW TO DEPLOY THIS APP
- Azure
- SQL Server
- ACR

### docker
- docker build -t expedia .
- docker run -d --name expediaAPI -p 8080:80 expedia

### need to change the database connection string
- docker inspect bridge
- replace "localhost" with "your_database_docker_ip(IPv4Address e.g., 172.17.0.2)"

### other docker
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
- 400 Bad Request, 401 Unauthorized, 403 Forbidden, 404 Not Found, 422 Unprocessable Entity Data
- 500 Internal Server Error, 502 Bad Gateway

### REST (Hatoas)
- Headers
    - Accept
        - application/xml
        - application/json
        - (custom) application/vnd.{companyName}.hateoas+json
    - Content-Type
- Patch (e.g.,)
    - add
    - remove
    - replace
    - move
    - copy
    - test
    [
        {"op": "replace", "path": "/title", "value": "abc"},
        {"op": "remove", "path": "/originalPrice"},
        {"op": "replace", "path": "/picture/url", "value": "../../images/123.jpg"},
    ]

### Add HEAD support
- no body
- support cache

### API attributes
- FromQuery
- FromBody
- FromFom
- FromRoute
- FromService

### Async & Await
- void, Task, Task<T>, IAsyncEnumerable<T>

### Authorization
- SSO (session(server) & cookie) for k8s
    - ForgeRock, Microsoft AM
    - OpenAM, OpenIDM, OpenDJ
- JWT (RSA)
    -

### Dynamic fields returns
- ExpandoObject

### Linq
- IQueryable
- System.Linq.Dynamic.Core (sorting dynamic fields)

### Log
- Console.WriteLine("+++++ Keyword: " + Keyword);

### Manage NuGet Packages
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Newtonsoft.Json -> parse json string, deserialized
- Pomelo.EntityFrameworkCore.MySql
- AutoMapper.Extensions.Microsoft.DependencyInjection -> dto mapping
- Microsoft.AspNetCore.JsonPatch -> "patch update" inputs
- Microsoft.AspNetCore.Mvc.NewtonsoftJson -> for json patch
- Microsoft.AspNetCore.Authentication.JwtBearer -> jwt support
- Microsoft.AspNetCore.Identity.EntityFrameworkCore -> user entity
- stateless -> order state
- System.Linq.Dynamic.Core -> for linq dynamic mapping orderBy (fields) from dto

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

### short-cut key
search: cmd + shift + f
build: cmd + option + enter
double click: collapse side bar

### video
- https://www.youtube.com/@studyworld1726