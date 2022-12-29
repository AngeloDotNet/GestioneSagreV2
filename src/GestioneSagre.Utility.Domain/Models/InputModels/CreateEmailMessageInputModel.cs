using System.ComponentModel.DataAnnotations;
using GestioneSagre.Utility.Infrastructure.Enum;

namespace GestioneSagre.Utility.Domain.Models.InputModels;

public class CreateEmailMessageInputModel
{
    [Required(ErrorMessage = "EmailId is required")] public Guid EmailId { get; set; }
    [Required(ErrorMessage = "Recipient is required")] public string Recipient { get; set; }
    [Required(ErrorMessage = "RecipientEmail is required")] public string RecipientEmail { get; set; }
    [Required(ErrorMessage = "Subject is required")] public string Subject { get; set; }
    [Required(ErrorMessage = "Message is required")] public string Message { get; set; }
    public DateTime SendDate { get; set; }
    public DateTime EffectiveSendDate { get; set; }
    public int EmailSendCount { get; set; }
    public EmailStatus Status { get; set; }

    public CreateEmailMessageInputModel(Guid emailId, string recipient, string recipientEmail, string subject, string message)
    {
        EmailId = emailId;
        Recipient = recipient;
        RecipientEmail = recipientEmail;
        Subject = subject;
        Message = message;
        SendDate = DateTime.Now;
        EffectiveSendDate = DateTime.Now;
        EmailSendCount = 0;
        Status = EmailStatus.Pending;
    }
}