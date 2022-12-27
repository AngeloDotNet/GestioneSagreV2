using Microsoft.Extensions.DependencyInjection;

namespace GestioneSagre.Tools.RabbitMQ.Abstractions;

public interface IMessagingBuilder
{
    IServiceCollection Services { get; }
}