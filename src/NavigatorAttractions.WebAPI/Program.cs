using AutoMapper;
using Elastic.Apm.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;
using NavigatorAttractions.Core.Configuration;
using NavigatorAttractions.Core.Constants;
using NavigatorAttractions.Core.Health;
using NavigatorAttractions.Core.Logging;
using NavigatorAttractions.Data.Interface;
using NavigatorAttractions.Data.Repository;
using NavigatorAttractions.Service.Profiles;
using NavigatorAttractions.Service.Services;
using NavigatorAttractions.Service.Services.Interface;
using NavigatorAttractions.WebAPI.Extentsions;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

SetupConfiguration();
SetupLogger();
SetupServices();
SetupMappings();
AddServices();
AddHealthCheckServices();

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
    services.AddControllers().AddNewtonsoftJson();

    //services.AddControllers(options =>
    //{
    //    options.InputFormatters.Insert(0, MyJPIF.GetJsonPatchInputFormatter());
    //});

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
        c.DocumentFilter<JsonPatchDocumentFilter>();
    });
    services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

    // Razor Pages
    services.AddRazorPages();
}

void SetupMappings()
{
    services.AddSingleton(provider => new MapperConfiguration(cfg =>
    {
        cfg.AddProfile(new AttractionProfile());
        //cfg.AddProfile(new MapProfile());
        cfg.AddProfile(new ReferenceTypesProfile());
    }).CreateMapper());
}

void AddServices()
{
    var config = configuration.Get<ApplicationOptions>();

    services.AddSingleton<IAttractionRepository>(new AttractionRepository(config.ConnectionStrings.MongoNavigator));
    services.AddSingleton<IPhotoRepository>(new PhotoRepository(config.ConnectionStrings.MongoNavigator));
    
    services.AddScoped<IAttractionService, AttractionService>();
    services.AddScoped<IPhotoService, PhotoService>();
}

void AddHealthCheckServices()
{
    var config = configuration.Get<ApplicationOptions>();
    var mongoConnectionString = config.ConnectionStrings.MongoNavigator;

    services
        .AddHealthChecksUI()
        .AddInMemoryStorage()
        .Services
        .AddHealthChecks()
        .AddVersionHealthCheck()
        .AddMongoDb(mongoConnectionString)
        .Services
        .AddControllers();
}

void SetupApp()
{
    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseSwagger();
    app.UseSwaggerUI();

    // Elastic APM
    var elasticEnabled = configuration.GetValue<bool>(ApplicationConstants.ElasticEnabled);
    if (elasticEnabled)
    {
        app.UseElasticApm(configuration);
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapRazorPages();

        endpoints.MapHealthChecks("healthz", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        });
    });

    var option = new RewriteOptions();
    option.AddRedirect("^$", "swagger");
    app.UseRewriter(option);

}