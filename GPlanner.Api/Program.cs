using GenerativeAI;
using GPlanner.Core.Repositories;
using GPlanner.Data;
using GPlanner.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using NSwag.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

var geminiApiKey = builder.Configuration.GetSection("Gemini:ApiKey").Value
    ?? throw new InvalidOperationException("Gemini API key not found in configuration.");

var googleAI = new GoogleAi(geminiApiKey);
var model = googleAI.CreateGenerativeModel("gemini-2.5-flash");

builder.Services.AddSingleton(model);

builder.Services.AddDbContext<GPlannerDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddControllers();
builder.Services.AddScoped<IUserTaskRepository, UserTaskRepository>();
builder.Services.AddScoped<IGeminiPlanningRepository, GeminiPlanningRepository>();
builder.Services.AddScoped<IDailyPlanRepoistory, DailyPlanRepository>();
builder.Services.AddScoped<IScheduledTaskRepository, ScheduledTaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "GPlanner API";
    config.Version = "v1";
});
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
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