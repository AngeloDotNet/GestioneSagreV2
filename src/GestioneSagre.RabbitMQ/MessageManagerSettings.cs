using System.Text.Json;
using GestioneSagre.RabbitMQ.Abstractions;

namespace GestioneSagre.RabbitMQ;

public class MessageManagerSettings
{
    public string ConnectionString { get; set; }
    public string ExchangeName { get; set; }
    public ushort QueuePrefetchCount { get; set; }
    public JsonSerializerOptions JsonSerializerOptions { get; set; } = JsonOptions.Default;
}