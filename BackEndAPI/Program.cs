using System.Text;
using BackEndAPI.Data;
using BackEndAPI.Extensions;
using BackEndAPI.Interfaces;
using BackEndAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// ====  Add services to the container ==== //
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration); //application service extension methods
builder.Services.AddIdentityServices(builder.Configuration); //identity service extension methods

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

//--- Configure the HTTP request pipeline ---//
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("https://localhost:4200")); //to allow cross domain requests

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
