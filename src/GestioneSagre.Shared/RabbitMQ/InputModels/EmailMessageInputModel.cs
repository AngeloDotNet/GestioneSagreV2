namespace GestioneSagre.Shared.RabbitMQ.InputModels;

public class EmailMessageInputModel
{
    public int Id { get; set; }
    public Guid EmailId { get; set; }
    public string RecipientEmail { get; set; }
    public string ReplyEmail { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
}