using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DatingApp.API.Services;
using DatingApp.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using DatingApp.API.Data;

namespace DatingApp.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddDbContext<DataContext>(x => x.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
}