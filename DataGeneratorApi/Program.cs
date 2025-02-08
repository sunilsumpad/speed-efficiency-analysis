using Microsoft.EntityFrameworkCore;
using DataGeneratorApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<BikeSpeedDataContext>(opt =>
    opt.UseInMemoryDatabase("BikeSpeedDataList"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Load customsettings.json
builder.Configuration.AddJsonFile("customsettings.json", optional: true, reloadOnChange: true);
// Bind CustomSettings section
builder.Services.Configure<CustomSettings>(builder.Configuration.GetSection("CustomSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
