using Microsoft.EntityFrameworkCore;
using NZWalksAPI.DBData;
using NZWalksAPI.Models;
using NZWalksAPI.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NzWalksDBContext>(options =>

{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalks"));
});
builder.Services.AddScoped<IRepository, RegionRepository>();
builder.Services.AddScoped<IWalksRepository, WalksRepository>();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
//Automapper Configuration
builder.Services.AddAutoMapper(typeof(Program).Assembly);

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
