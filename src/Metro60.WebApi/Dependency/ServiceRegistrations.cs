using Metro60.Core.Data;
using Metro60.Core.Handler;
using Metro60.Core.Services;
using Metro60.WebApi.Controllers.Examples;
using Metro60.WebApi.Health;
using Metro60.WebApi.Logging;

using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.Filters;

namespace Metro60.WebApi.Dependency;

public static class ServiceRegistrations
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Host.AddLogger();
        builder.Services.AddDbContext<MetroDbContext>();
        builder.Services.AddAuthentication("BasicAuthentication")
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

        builder.Services.AddSingleton<IHealthProbe, HealthProbe>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("MyAllowSpecificOrigins",
                policy =>
                {
                    policy
                        .WithOrigins("http://127.0.0.1:3000", "http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .AllowAnyMethod();
                });
        });
        builder.Services.AddSwaggerGen(c =>
        {
            c.ExampleFilters();
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Metro60 Product Api", Version = "v1" });
            c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "basic",
                In = ParameterLocation.Header,
                Description = "Basic Authorization header using the Bearer scheme."
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "basic"
                        }
                    },
                    new string[] {}
                }
            });
        });
        builder.Services.AddSwaggerExamplesFromAssemblyOf<AddProductExamples>();
        builder.Services.AddHealthChecks().AddCheck<ServiceHealthCheck>(nameof(ServiceHealthCheck));

        return builder;
    }
}
