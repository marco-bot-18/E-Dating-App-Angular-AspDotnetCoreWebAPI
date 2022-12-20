using BackEndAPI.Data;
using BackEndAPI.Entities;
using BackEndAPI.Extensions;
using BackEndAPI.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ====  Add services to the container ==== //

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration); //application service extension methods
builder.Services.AddIdentityServices(builder.Configuration); //identity service extension methods

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

/*if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}*/

/*
    *Configure the HTTP request pipeline and middlewares
*/

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(builder => builder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("https://localhost:4200")); /* to allow cross domain requests */

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//for seeding data into the database
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

    await context.Database.MigrateAsync();
    await Seed.SeedUsers(userManager, roleManager);
}
catch (Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration!");
}

app.Run();
