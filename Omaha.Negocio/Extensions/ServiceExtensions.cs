using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Omaha.Infra.Context;
using Omaha.Negocio.Interfaces;
using Omaha.Negocio.Services;
using Omaha.Negocio.Settings;

namespace Omaha.Negocio.Extensions
{
    public static class ServiceExtensions
    {
        public static void InterfacesService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OmahaContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("ConexionDB-EF")), ServiceLifetime.Transient);            
            services.AddTransient<IUserLogin, UserLoginService>();
            services.AddTransient<IPdfList, PdfListService>();
            services.AddTransient<IProfile, ProfilePicService>();
            services.Configure<JWT>(configuration.GetSection("JWT"));

        }
    }
}
