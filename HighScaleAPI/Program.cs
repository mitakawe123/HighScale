using FastEndpoints;
using FastEndpoints.Swagger;
using HighScale.Application.Configurations;
using HighScale.Infrastructure.Configurations;

var bld = WebApplication.CreateBuilder();
bld.Services
    .AddFastEndpoints()
    .SwaggerDocument()
    .AddAppServices()
    .AddScyllaDbServices(bld.Configuration)
    .AddDragonFlyDbServices(bld.Configuration);

var app = bld.Build();
app.UseFastEndpoints()
    .UseSwaggerGen(); 

app.Run();