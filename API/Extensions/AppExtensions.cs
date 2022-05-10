using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using API.interfaces;
using API.Services;
using API.Data;
namespace API.Extensions
{
    public static class AppExtensions
    {
        public static IServiceCollection AddAppservice(this IServiceCollection services, IConfiguration Config)
        {
            services.AddScoped<ITokens, Tokens>();
            services.AddDbContext<DataContext>( Options =>
            {
                Options.UseSqlite(Config.GetConnectionString("DefualtConnection"));

            });
            return services;

        }
    }
}