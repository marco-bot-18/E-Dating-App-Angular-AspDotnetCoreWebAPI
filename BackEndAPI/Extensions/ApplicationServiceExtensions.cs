using BackEndAPI.Data;
using BackEndAPI.Helpers;
using BackEndAPI.Interfaces;
using BackEndAPI.Services;
using BackEndAPI.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BackEndAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // services.AddDbContext<DataContext>(opt =>
            // {
            //     opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            // }); //*remove this in terms of deployment

            services.AddCors(); //to allow cross domain requests
            services.AddScoped<ITokenService, TokenService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<LogUserActivity>();
            services.AddSignalR();
            services.AddSingleton<PresenceTracker>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}