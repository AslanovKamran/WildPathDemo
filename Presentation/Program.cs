using Microsoft.OpenApi.Models;
using Presentation.Extensions;
using System.Reflection;
using Infrastructure.Databases.Abstract;
using Infrastructure.Databases.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen((options) =>
{
    #region Documentation Section

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Wild Path",
        Version = "v1",
        Description = "API documentation"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    #endregion
});


//Since database connections are short-lived and should not be shared across requests, you should use AddScoped instead of AddSingleton.
//Reason: Singleton lifetime means the service is created once and shared throughout the application's lifetime, which can cause issues with connection management.
builder.Services.AddScoped<IDatabase, PostgresDbConnection>();

builder.Services.AddRepositories();
builder.Services.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.DisplayRequestDuration(); });

}

// In case we may need to use photos from the server
app.UseStaticFiles();


//Preventing Cross-Origin Request blocking
app.UseCors(options =>
{
    options.AllowAnyMethod();
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
