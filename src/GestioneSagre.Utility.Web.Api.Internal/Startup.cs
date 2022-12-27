using GestioneSagre.Tools.MailKit;
using GestioneSagre.Tools.MailKit.Options;
using GestioneSagre.Utility.Domain.Mapping;
using GestioneSagre.Utility.Infrastructure.DataAccess;
using GestioneSagre.Utility.Web.Api.Internal.HostedServices;
using GestioneSagre.Utility.Web.Api.Internal.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace GestioneSagre.Utility.Web.Api.Internal;

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
            options.AddPolicy("UtilityInternalApi", policy =>
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
                Title = "Gestione Sagre internal API - Utility",
                Version = "v1"
            });
        });

        services.AddDbContext<DataDbContext>(options =>
            options.UseSqlServer(Configuration.GetSection("ConnectionStrings").GetValue<string>("Database")));

        services.AddSingleton<EmailSenderHostedService>();
        services.AddSingleton<ISendEmailServices>(provider => provider.GetRequiredService<ISendEmailServices>());
        services.AddSingleton<IHostedService>(provider => provider.GetRequiredService<EmailSenderHostedService>());

        services.AddTransient<ISendEmailServices, SendEmailServices>();

        services.AddSingleton<IEmailSender, MailKitEmailSender>();
        services.AddSingleton<IEmailClient, MailKitEmailSender>();

        services.Configure<SmtpOptions>(Configuration.GetSection("Smtp"));

        services.AddAutoMapper(typeof(EmailMessageMapperProfile));
    }

    public void Configure(WebApplication app)
    {
        IWebHostEnvironment env = app.Environment;

        app.UseHttpsRedirection();
        app.UseCors("UtilityInternalApi");

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = string.Empty;
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestione Sagre internal API V1 - Utility");
        });

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}