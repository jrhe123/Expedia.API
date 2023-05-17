using Expedia.API.Database;
using Expedia.API.Services;

var builder = WebApplication.CreateBuilder(args);

// register services into IoC container
// to use api, we need mvc controller
builder.Services.AddControllers();
// register repository
// 1. AddTransient: every request
// 2. AddSingleton: app init
// 3. AddScoped: transaction
builder.Services.AddTransient<ITouristRouteRepository, MockTouristRouteRepository>();
// register db context
builder.Services.AddDbContext<AppDbContext>(option =>
{

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