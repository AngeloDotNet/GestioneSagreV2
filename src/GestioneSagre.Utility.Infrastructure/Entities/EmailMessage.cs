using GestioneSagre.Shared.Entities;
using GestioneSagre.Utility.Infrastructure.Enum;

namespace GestioneSagre.Utility.Infrastructure.Entities;

public class EmailMessage : BaseEntity
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

    public EmailMessage(int id, Guid emailId, string recipient, string recipientEmail, string subject,
        string message, DateTime sendDate, DateTime effectiveSendDate, int emailSendCount, EmailStatus status)
    {
        Id = id;
        EmailId = emailId;
        Recipient = recipient;
        RecipientEmail = recipientEmail;
        Subject = subject;
        Message = message;
        SendDate = sendDate;
        EffectiveSendDate = effectiveSendDate;
        EmailSendCount = emailSendCount;
        Status = status;
    }
}