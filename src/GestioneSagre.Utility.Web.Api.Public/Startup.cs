using GestioneSagre.Utility.Business;
using GestioneSagre.Utility.Business.Handlers.Read;
using GestioneSagre.Utility.Domain.Mapping;
using GestioneSagre.Utility.Infrastructure.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace GestioneSagre.Utility.Web.Api.Public;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddCors(options =>
        {
            options.AddPolicy("UtilityPublicApi", policy =>
            {
                policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Gestione Sagre public API - Utility",
                Version = "v1"
            });
        });

        var connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Database");

        services.AddDbContext<DataDbContext>(options =>
            options.UseSqlite(connectionString, migration =>
                migration.MigrationsAssembly(typeof(DataDbContext).Assembly.FullName))
        );

        services.AddApplicationCore();
        services.AddInfrastructure();

        var connectionRabbitMQ = Configuration.GetSection("ConnectionStrings").GetValue<string>("RabbitMq");
        var exchangeName = Configuration.GetValue<string>("RabbitMq:ApplicationName");
        var queuePrefetchCount = Configuration.GetValue<ushort>("RabbitMq:QueuePrefetchCount");

        services.AddRabbitMqService(connectionRabbitMQ, exchangeName, queuePrefetchCount);

        services.AddAutoMapper(typeof(EmailMessageMapperProfile));
        services.AddMediatR(typeof(GetEmailMessagesListHandler).Assembly);
    }

    public void Configure(WebApplication app)
    {
        IWebHostEnvironment env = app.Environment;

        app.UseHttpsRedirection();
        app.UseCors("UtilityPublicApi");

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestione Sagre public API V1 - Utility");
        });

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}