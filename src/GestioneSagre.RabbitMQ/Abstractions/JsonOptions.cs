using System.Text.Json;
using System.Text.Json.Serialization;

namespace GestioneSagre.RabbitMQ.Abstractions;

internal static class JsonOptions
{
    public static JsonSerializerOptions Default { get; } = new(JsonSerializerDefaults.Web)
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    };
}