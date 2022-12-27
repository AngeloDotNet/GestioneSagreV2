using Microsoft.Extensions.DependencyInjection;

namespace GestioneSagre.Tools.RabbitMQ.Abstractions;

internal class DefaultMessagingBuilder : IMessagingBuilder
{
    public IServiceCollection Services { get; }

    public DefaultMessagingBuilder(IServiceCollection services)
    {
        Services = services;
    }
}