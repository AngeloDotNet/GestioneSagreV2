namespace GestioneSagre.Tools.RabbitMQ.Abstractions;

public interface IMessageReceiver<T> where T : class
{
    Task ReceiveAsync(T message, CancellationToken cancellationToken);
}