using AutoMapper;
using Elastic.Apm.AspNetCore;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using NavigatorAttractions.Core.Configuration;
using NavigatorAttractions.Core.Logging;
using NavigatorAttractions.Data.Interface;
using NavigatorAttractions.Data.Repository;
using NavigatorAttractions.Service.Services;
using NavigatorAttractions.Service.Services.Interface;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

SetupConfiguration();
SetupLogger();
SetupServices();
SetupMappings();
AddServices();

var app = builder.Build();
SetupApp();
app.Run();

void SetupConfiguration()
{
    services.AddOptions();
    services.Configure<ApplicationOptions>(configuration);
    services.AddSingleton(configuration);

    var config = configuration.Get<ApplicationOptions>();

}

void SetupLogger()
{
    builder.Host.UseSerilog(Logging.ConfigureLogger);
}

void SetupServices()
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();

    // Swagger
    services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "oauth2 Access token to authenticate and authorize the user",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
        var xmlFilePath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");

        c.ExampleFilters();
        c.IncludeXmlComments(xmlFilePath);
    });
    services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
}

void SetupMappings()
{
    services.AddSingleton(provider => new MapperConfiguration(cfg =>
    {
        //cfg.AddProfile(new AttractionProfile());
        //cfg.AddProfile(new MapProfile());
    }).CreateMapper());
}

void AddServices()
{
    var config = configuration.Get<ApplicationOptions>();

    services.AddSingleton<IAttractionRepository>(new AttractionRepository(config.ConnectionStrings.MongoNavigator));

    builder.Services.AddScoped<IAttractionService, AttractionService>();
}

void SetupApp()
{
    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseSwagger();
    app.UseSwaggerUI();

    // Elastic APM
    app.UseElasticApm(configuration);

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    var option = new RewriteOptions();
    option.AddRedirect("^$", "swagger");
    app.UseRewriter(option);

}