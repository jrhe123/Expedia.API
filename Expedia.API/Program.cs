using Microsoft.Extensions.Configuration;
using Expedia.API.Database;
using Expedia.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Expedia.API.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetValue<string>(
    "DbContext:ConnectionString"
    );
var jwtSecretKey = builder.Configuration.GetValue<string>(
    "Authenitication:SecretKey"
    );
var jwtValidIssuer = builder.Configuration.GetValue<string>(
    "Authenitication:Issuer"
    );
var jwtValidAudience = builder.Configuration.GetValue<string>(
    "Authenitication:Audience"
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
    .AddNewtonsoftJson(setupAction =>
    {
        // for "JsonPatchDocument" patch api
        setupAction.SerializerSettings.ContractResolver =
            new CamelCasePropertyNamesContractResolver();
    })
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

// jwt
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<AppDbContext>(); // migrate user tables
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var secretByte = Encoding.UTF8.GetBytes(jwtSecretKey);
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = jwtValidIssuer,
            ValidateAudience = true,
            ValidAudience = jwtValidAudience,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretByte)
        };
    });

// register url helper
builder.Services.AddSingleton<
    IActionContextAccessor, ActionContextAccessor>();

// property mapping service for custom "order by fields"
builder.Services.AddTransient<IPropertyMappingService, PropertyMappingService>();

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

// enable jwt
app.UseAuthentication();
app.UseAuthorization();

// mvc
app.MapControllers();

app.MapGet("/", () => "Hello World!");
app.Run();