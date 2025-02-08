using Microsoft.EntityFrameworkCore;
using SpeedVsEfficiencyApi.Models;

var CustomAllowSpecificOrigins = "_customAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
// SQLite Database Provider Setup
// Connection string
string? connectionString = builder.Configuration.GetConnectionString("BikeSpeedDatas") ?? "Data Source=BikeSpeedData.db";
// Add database context using SQLite
builder.Services.AddSqlite<BikeSpeedDataContext>(connectionString);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CustomAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173",
                                              "http://localhost:5117")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(CustomAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
