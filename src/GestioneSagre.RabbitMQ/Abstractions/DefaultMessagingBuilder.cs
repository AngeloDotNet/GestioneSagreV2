using Microsoft.Extensions.DependencyInjection;

namespace GestioneSagre.RabbitMQ.Abstractions;

internal class DefaultMessagingBuilder : IMessagingBuilder
{
    public IServiceCollection Services { get; }

    public DefaultMessagingBuilder(IServiceCollection services)
    {
        Services = services;
    }
}