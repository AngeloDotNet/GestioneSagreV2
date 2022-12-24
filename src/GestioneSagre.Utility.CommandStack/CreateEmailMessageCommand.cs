using GestioneSagre.Utility.Domain.Models.InputModels;
using GestioneSagre.Utility.Infrastructure.Enum;
using MediatR;

namespace GestioneSagre.Utility.CommandStack;

public class CreateEmailMessageCommand : IRequest<bool>
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

    public CreateEmailMessageCommand(CreateEmailMessageInputModel inputModel)
    {
        EmailId = inputModel.EmailId;
        Recipient = inputModel.Recipient;
        RecipientEmail = inputModel.RecipientEmail;
        Subject = inputModel.Subject;
        Message = inputModel.Message;
        SendDate = inputModel.SendDate;
        EffectiveSendDate = inputModel.EffectiveSendDate;
        EmailSendCount = inputModel.EmailSendCount;
        Status = inputModel.Status;
    }
}