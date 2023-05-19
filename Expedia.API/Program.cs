using Microsoft.Extensions.Configuration;
using Expedia.API.Database;
using Expedia.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetValue<string>(
    "DbContext:ConnectionString"
    );
string mysqlConnectionString = builder.Configuration.GetValue<string>(
    "DbContext:MySQLConnectionString"
    );

// register services into IoC container
// to use api, we need mvc controller
builder.Services.AddControllers();
// register repository
// 1. AddTransient: every request
// 2. AddSingleton: app init
// 3. AddScoped: transaction
// mock data
// builder.Services.AddTransient<ITouristRouteRepository, MockTouristRouteRepository>();
builder.Services.AddTransient<ITouristRouteRepository, TouristRouteRepository>();

// register db context
builder.Services.AddDbContext<AppDbContext>(option =>
{
    // load the connection string from "appsettings.json"
    // sql server
    // option.UseSqlServer(connectionString);

    // mysql
    option.UseMySql(
        mysqlConnectionString,
        ServerVersion.AutoDetect(mysqlConnectionString)
        );
});


//
var app = builder.Build();

// environment variables
// path: Properties/launchSettings.json
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


// http request pipeline (middleware)
// e.g., logging <-> static file <-> mvc
// e.g., UseRouting <-> UseEndpoints
// deprecated version
app.UseRouting();
//app.UseEndpoints(endPoints =>
//{
//    endPoints.MapGet("/test", async context =>
//    {
//        await context.Response.WriteAsync("this is test api 123");
//    });
//    endPoints.MapControllers();
//});

//app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Hello World!");
app.Run();