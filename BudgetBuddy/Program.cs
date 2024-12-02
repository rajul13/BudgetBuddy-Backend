using BudgetBuddy.DAL.DBContext;
using BudgetBuddy.Service.Mappers;
using BudgetBuddy.Service.ServicesWireup;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connString = builder.Configuration.GetConnectionString("BudgetBuddyConnection");
builder.Services.AddDbContext<BudgetBuddyDbContext>(options => options.UseNpgsql(connString));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var applicationName = builder.Configuration.GetValue<string>("ApplicationSettings:ApplicationName");
    c.SwaggerDoc("v1", new OpenApiInfo { Title = applicationName });
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

//Wireup services with concrete class
builder.Services.RegisterDependencies();




var app = builder.Build();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (builder.Configuration.GetValue<string>("ApplicationSettings:EnableSwaggerUI").ToLower() == "true")
{
    // Enable middleware to serve generated Swagger as a JSON endpoint
    app.UseSwagger();
    // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
