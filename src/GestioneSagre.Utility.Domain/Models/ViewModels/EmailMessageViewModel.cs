using GestioneSagre.Shared.Entities;
using GestioneSagre.Utility.Infrastructure.Entities;
using GestioneSagre.Utility.Infrastructure.Enum;

namespace GestioneSagre.Utility.Domain.Models.ViewModels;

public class EmailMessageViewModel : BaseEntity
{
    public Guid EmailId { get; set; }
    public string Recipient { get; set; }
    public string RecipientEmail { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
    public DateTime SendDate { get; set; }
    public DateTime EffectiveSendDate { get; set; }
    public int EmailSendCount { get; set; }
    public EmailStatus Status { get; set; }

    public static EmailMessageViewModel FromEntity(EmailMessage message)
    {
        return new EmailMessageViewModel
        {
            Id = message.Id,
            EmailId = message.EmailId,
            Recipient = message.Recipient,
            RecipientEmail = message.RecipientEmail,
            Subject = message.Subject,
            Message = message.Message,
            SendDate = message.SendDate,
            EffectiveSendDate = message.EffectiveSendDate,
            EmailSendCount = message.EmailSendCount,
            Status = message.Status
        };
    }
}