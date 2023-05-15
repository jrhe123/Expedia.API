var builder = WebApplication.CreateBuilder(args);

// register services into IoC container
// to use api, we need mvc controller
builder.Services.AddControllers();

//
var app = builder.Build();

// environment variables
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


// http request pipeline (middleware)
// e.g., logging <-> static file <-> mvc
// e.g., UseRouting <-> UseEndpoints
// deprecated version
app.UseRouting();
app.UseEndpoints(endPoints =>
{
    endPoints.MapGet("/test", async context =>
    {
        await context.Response.WriteAsync("this is test api 123");
    });
});



app.MapGet("/", () => "Hello World!");
app.Run();