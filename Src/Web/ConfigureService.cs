using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Web
{
    public static class ConfigureService
    {
        public static IServiceCollection AddWebServiceCollection(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<DataBaseContext>(opt => opt.UseSqlServer("Data Source=DESKTOP-C45ID5O\\MAHDI;Integrated Security=True;Initial Catalog=Online-shop;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"));

            return builder.Services;
        }
    }
}
