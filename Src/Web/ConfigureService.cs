using Infrastructure.Persistance.Context;
using Infrastructure.Persistance.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Web
{
    public static class ConfigureService
    {
        public static IServiceCollection AddWebServiceCollection(this WebApplicationBuilder builder)
        {

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            return builder.Services;
        }

        public static async Task<IApplicationBuilder> AddWebAppService(this WebApplication app)
        {

            #region Get Services

            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var context = services.GetRequiredService<DataBaseContext>();


            try
            {
                await context.Database.MigrateAsync();
                await GenerateFakeData.SeedDataAsync(context, loggerFactory);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, ex.Message);
            }

            #endregion


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
            return app;
        }
    }
}
