using BackEndAPI.Extensions;
using BackEndAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ====  Add services to the container ==== //

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration); //application service extension methods
builder.Services.AddIdentityServices(builder.Configuration); //identity service extension methods

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// if (builder.Environment.IsDevelopment())
// {
//     app.UseDeveloperExceptionPage();
// }

//--- Configure the HTTP request pipeline and middlewares ---//

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(builder => builder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("https://localhost:4200")); //to allow cross domain requests

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
