
var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.ConfigureSqlConnections(configuration);
builder.Services.AddDependencies();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowedToAllowWildcardSubdomains();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

// Disabled for testing purposes
//app.UseWhen(context => context.Request.Path.StartsWithSegments("/api"),
//    appBuilder =>
//    {
//        appBuilder.UseMiddleware<ApiKeyMiddleware>();
//    });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
