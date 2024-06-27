using Application.Contracts;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection Services , 
            IConfiguration configuration)
        {
            Services.AddDbContext<DataBaseContext>(opt => opt.UseSqlServer("Data Source=DESKTOP-C45ID5O\\MAHDI;Integrated Security=True;Initial Catalog=Online-shop;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"));
            //Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            return Services;
        }
    }
} 
