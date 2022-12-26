namespace GestioneSagre.Shared.Models.InputModels;

public class EmailInputModel
{
    public string RecipientEmail { get; set; }
    public string ReplyEmail { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
}
