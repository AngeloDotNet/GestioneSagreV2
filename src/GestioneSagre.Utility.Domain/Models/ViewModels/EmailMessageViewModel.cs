using GestioneSagre.Utility.Infrastructure.Enum;

namespace GestioneSagre.Utility.Domain.Models.ViewModels;

public class EmailMessageViewModel
{
    public int Id { get; set; }
    public Guid EmailId { get; set; }
    public string Recipient { get; set; }
    public string RecipientEmail { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
    public DateTime SendDate { get; set; }
    public DateTime EffectiveSendDate { get; set; }
    public int EmailSendCount { get; set; }
    public EmailStatus Status { get; set; }
}