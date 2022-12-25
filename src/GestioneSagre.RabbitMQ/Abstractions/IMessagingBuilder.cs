using Microsoft.Extensions.DependencyInjection;

namespace GestioneSagre.RabbitMQ.Abstractions;

public interface IMessagingBuilder
{
    IServiceCollection Services { get; }
}