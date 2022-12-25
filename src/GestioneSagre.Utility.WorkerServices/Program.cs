using GestioneSagre.RabbitMQ;
using GestioneSagre.Utility.Domain.Models.InputModels;
using GestioneSagre.Utility.WorkerServices.Receivers;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(ConfigureServices)
    .Build();

await host.RunAsync();

void ConfigureServices(HostBuilderContext hostingContext, IServiceCollection services)
{
    // Configures the application message receivers.
    var configuration = hostingContext.Configuration;

    services.AddRabbitMq(settings =>
    {
        settings.ConnectionString = configuration.GetSection("ConnectionStrings").GetValue<string>("RabbitMq");
        settings.ExchangeName = configuration.GetValue<string>("RabbitMq:ApplicationName");
        settings.QueuePrefetchCount = configuration.GetValue<ushort>("RabbitMq:QueuePrefetchCount");
    }, queues =>
    {
        queues.Add<CreateEmailMessageInputModel>();
    })
    .AddReceiver<CreateEmailMessageInputModel, EmailMessageReceiver>();
}