using GestioneSagre.Utility.Infrastructure.Entities;

namespace GestioneSagre.Utility.Domain.Services.Write;

public interface ISendEmailWriteService
{
    Task<bool> CreateEmailMessage(EmailMessage request);
    Task<bool> UpdateEmailMessage(EmailMessage request);
    Task<bool> DeleteEmailMessage(int id, Guid emailId);
}