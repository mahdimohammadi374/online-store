using Application;
using Infrastructure;
using Infrastructure.Persistance;
using Infrastructure.Persistance.SeedData;
using Microsoft.EntityFrameworkCore;
using Web;

var builder = WebApplication.CreateBuilder(args);



#region Configuration Extention
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureService(builder.Configuration);
builder.AddWebServiceCollection();

#endregion


var app = builder.Build(); 
await app.AddWebAppService();