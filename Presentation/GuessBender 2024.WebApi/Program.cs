using GuessBender_2024.Application.Interfaces;
using GuessBender_2024.Persistance.Context;
using GuessBender_2024.Persistance.Repositories;
using GuessBender_2024.Application.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using GuessBender_2024.Application.Tools;

using System.Text;
using Microsoft.OpenApi.Models;
using GuessBender_2024.Application.Interfaces.AuthorizationInterfaces;
using GuessBender_2024.Persistance.Repositories.AuthorizationRepositories;
using GuessBender_2024.Application.Interfaces.StandingInterfaces;
using GuessBender_2024.Persistance.Repositories.StandingRepositories;
using GuessBender_2024.Application.Interfaces.MatchInterfaces;
using GuessBender_2024.Persistance.Repositories.MatchRepositories;
using GuessBender_2024.Application.Interfaces.LeagueStandingInterfaces;
using GuessBender_2024.Persistance.Repositories.LeagueStandingRepositories;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(opt =>
{
	opt.AddPolicy("CorsPolicy", builder =>
	{
		builder.AllowAnyHeader()
		.AllowAnyMethod()
		.SetIsOriginAllowed((host) => true)
		.AllowCredentials();
	});
});
;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{

    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidAudience = JwtTokenDefaults.ValidAudience,
        ValidIssuer = JwtTokenDefaults.ValidIssuer,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key)),
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

    };
});

// Add services to the container.
builder.Services.AddScoped<GuessBenderContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IMatchRepository), typeof(MatchRepository));
builder.Services.AddScoped(typeof(IAuthorizationRepository), typeof(AuthorizationRepository));
builder.Services.AddScoped(typeof(IStandingRepository), typeof(StandingRepository));
builder.Services.AddScoped(typeof(ILeagueStandingRepository), typeof(LeagueStandingRepository));




builder.Services.AddApplicationService(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
