using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using products.Application.Infrastructure;
using products.Application.Interfaces;
using products.Domain.Entities;
using products.Infrastructure.Persistence;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

//Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
//Services
builder.Services.AddScoped<IFileUploader, FileUploader>();


//DbContext
builder.Services.AddDbContext<IAppDbContext, ApplicationDbContext>(
dbContextOptions => dbContextOptions
   .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
   .LogTo(Console.WriteLine, LogLevel.Information)
   .EnableSensitiveDataLogging()
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
   .EnableDetailedErrors());
#region JWT

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "produ",
            ValidAudience = "produ",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecrateKeyforapp12"))

        };
    });
#endregion
// Add MediatR
builder.Services.AddMediatR(typeof(products.Application.Auth.Commands.RegisterCommand).Assembly);



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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
