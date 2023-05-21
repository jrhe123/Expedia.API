using Microsoft.Extensions.Configuration;
using Expedia.API.Database;
using Expedia.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetValue<string>(
    "DbContext:ConnectionString"
    );

//var mysqlConnectionString = builder.Configuration.GetValue<string>(
//    "DbContext:MySQLConnectionString"
//    );


// register services into IoC container
// to use api, we need mvc controller
builder.Services.AddControllers(
    setupAction =>
    {
        // enable xml as response
        setupAction.ReturnHttpNotAcceptable = true;
        //setupAction.OutputFormatters.Add(
        //    new XmlDataContractSerializerOutputFormatter()
        //);
    }
    )
    .AddXmlDataContractSerializerFormatters()
    .ConfigureApiBehaviorOptions(setAction =>
    {
        // custom validate input context message & status code
        setAction.InvalidModelStateResponseFactory = context =>
        {
            var problemDetail = new ValidationProblemDetails(context.ModelState)
            {
                Type = "VALIDATE_PARAMS",
                Title = "Data validation error",
                Status = StatusCodes.Status422UnprocessableEntity,
                Detail = "Please check errors",
                Instance = context.HttpContext.Request.Path
            };
            problemDetail.Extensions.Add(
                "traceId", context.HttpContext.TraceIdentifier
                );
            return new UnprocessableEntityObjectResult(problemDetail)
            {
                ContentTypes = {"application/problem+json"}
            };
        };
    });
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
     option.UseSqlServer(connectionString);

    // mysql
    //option.UseMySql(
    //    mysqlConnectionString,
    //    ServerVersion.AutoDetect(mysqlConnectionString)
    //    );
});

// register profiles => handles repo & dto mapping (auto mapper)
// scan all the profiles under "Profiles" folder
builder.Services.AddAutoMapper(
    AppDomain.CurrentDomain.GetAssemblies()
    );


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

// mvc
app.MapControllers();

app.MapGet("/", () => "Hello World!");
app.Run();