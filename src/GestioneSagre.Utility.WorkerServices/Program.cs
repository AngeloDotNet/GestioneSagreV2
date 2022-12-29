using GestioneSagre.Shared.RabbitMQ.InputModels;
using GestioneSagre.Tools.RabbitMQ;
using GestioneSagre.Utility.Business;
using GestioneSagre.Utility.Infrastructure.DataAccess;
using GestioneSagre.Utility.WorkerServices.Receivers;
using Microsoft.EntityFrameworkCore;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(ConfigureServices)
    .Build();

await host.RunAsync();

void ConfigureServices(HostBuilderContext hostingContext, IServiceCollection services)
{
    var configuration = hostingContext.Configuration;

    services.AddRabbitMq(settings =>
    {
        settings.ConnectionString = configuration.GetConnectionString("RabbitMQ");
        settings.ExchangeName = configuration.GetValue<string>("RabbitMqSettings:ApplicationName");
        settings.QueuePrefetchCount = configuration.GetValue<ushort>("RabbitMqSettings:QueuePrefetchCount");
    }, queues =>
    {
        queues.Add<EmailMessageInputModel>();
    })
        .AddReceiver<EmailMessageInputModel, EmailMessageReceiver>();

    services.AddDbContext<DataDbContext>(options =>
            options.UseSqlServer(configuration.GetSection("ConnectionStrings").GetValue<string>("Database"),
                migration => migration.MigrationsAssembly("GestioneSagre.Utility.Infrastructure"))
    );

    services.AddWorkerService();
}