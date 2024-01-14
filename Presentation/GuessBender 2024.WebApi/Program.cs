using GuessBender_2024.Application.Interfaces;
using GuessBender_2024.Persistance.Context;
using GuessBender_2024.Persistance.Repositories;
using GuessBender_2024.Application.Services;
using GuessBender_2024.Application.Interfaces.TeamInterfaces;
using GuessBender_2024.Persistance.Repositories.TeamRepositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<GuessBenderContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IMatchRepository), typeof(MatchRepository));

builder.Services.AddApplicationService(builder.Configuration);
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

app.UseAuthorization();

app.MapControllers();

app.Run();
