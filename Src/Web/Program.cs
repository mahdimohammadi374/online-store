using Infrastructure;
using Infrastructure.Persistance;
using Infrastructure.Persistance.SeedData;
using Microsoft.EntityFrameworkCore;
using Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Configuration Extention

builder.Services.AddInfrastructureService(builder.Configuration);
builder.AddWebServiceCollection();

#endregion


var app = builder.Build(); 
await app.AddWebAppService();