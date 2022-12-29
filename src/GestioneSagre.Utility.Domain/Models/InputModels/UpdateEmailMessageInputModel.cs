using System.ComponentModel.DataAnnotations;
using GestioneSagre.Shared.Entities;
using GestioneSagre.Utility.Infrastructure.Enum;

namespace GestioneSagre.Utility.Domain.Models.InputModels;

public partial class UpdateEmailMessageInputModel : BaseEntity
{
    [Required(ErrorMessage = "EmailId is required")] public Guid EmailId { get; set; }
    [Required(ErrorMessage = "Recipient is required")] public string Recipient { get; set; }
    [Required(ErrorMessage = "RecipientEmail is required")] public string RecipientEmail { get; set; }
    [Required(ErrorMessage = "Subject is required")] public string Subject { get; set; }
    [Required(ErrorMessage = "Message is required")] public string Message { get; set; }
    [Required(ErrorMessage = "SendDate is required")] public DateTime SendDate { get; set; }
    [Required(ErrorMessage = "EffectiveSendDate is required")] public DateTime EffectiveSendDate { get; set; }
    [Required(ErrorMessage = "EmailSendCount is required")] public int EmailSendCount { get; set; }
    [Required(ErrorMessage = "Status is required")] public EmailStatus Status { get; set; }

    public UpdateEmailMessageInputModel(int id, Guid emailId, string recipient, string recipientEmail, string subject,
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