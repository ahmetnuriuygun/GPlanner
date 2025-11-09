using GPlanner.Data;
using Microsoft.EntityFrameworkCore;
using NSwag.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<GPlannerDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddControllers();
builder.Services.AddScoped<IUserTaskRepository, UserTaskRepository>();

builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "GPlanner API";
    config.Version = "v1";
});


var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();

    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "GPlanner API Documentation";
    });
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();