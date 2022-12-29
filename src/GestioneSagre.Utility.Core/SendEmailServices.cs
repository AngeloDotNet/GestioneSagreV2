using GestioneSagre.Utility.Domain.Models.ViewModels;
using GestioneSagre.Utility.Infrastructure.DataAccess;
using GestioneSagre.Utility.Infrastructure.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GestioneSagre.Utility.Core;

public class SendEmailServices : ISendEmailServices
{
    private readonly ILogger<SendEmailServices> logger;
    private readonly DataDbContext dbContext;

    public SendEmailServices(ILogger<SendEmailServices> logger, DataDbContext dbContext)
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }

    public async Task<List<EmailMessageViewModel>> GetAllEmailMessagesAsync()
    {
        try
        {
            logger.LogInformation("GetAllEmailMessagesAsync - Start");
            var baseQuery = dbContext.EmailMessages
                .Where(x => x.Status == EmailStatus.Pending)
                .AsNoTracking();

            logger.LogInformation("GetAllEmailMessagesAsync - End");
            var messages = await baseQuery
                .Select(email => EmailMessageViewModel.FromEntity(email))
                .ToListAsync();

            return messages;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error reading email messages");
            throw;
        }
    }

    public async Task<EmailMessageViewModel> GetEmailMessageAsync(Guid emailId)
    {
        try
        {
            logger.LogInformation("GetEmailMessageAsync - Start");
            var baseQuery = dbContext.EmailMessages
                .AsNoTracking()
                .Where(x => x.EmailId == emailId)
                .Select(email => EmailMessageViewModel.FromEntity(email));

            logger.LogInformation("GetEmailMessageAsync - End");
            var message = await baseQuery.FirstOrDefaultAsync();

            return message;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error reading email message");
            throw;
        }
    }

    public async Task<bool> UpdateEmailStatusAsync(int id, Guid emailId, int status)
    {
        try
        {
            logger.LogInformation("UpdateEmailStatusAsync - Start");
            var entity = await dbContext.EmailMessages.FindAsync(id);

            if (entity == null || entity.EmailId != emailId)
            {
                logger.LogWarning("Email not found");
                return false;
            }

            if (status == 0)
            {
                logger.LogInformation("Email status set to Pending");
                entity.Status = EmailStatus.Pending;
            }

            if (status == 1)
            {
                logger.LogInformation("Email status set to Sent");
                entity.EffectiveSendDate = DateTime.Now;
                entity.Status = EmailStatus.Sent;
            }

            if (status == 2)
            {
                logger.LogInformation("Email status set to Failed");
                entity.Status = EmailStatus.Failed;
            }

            if (status == 3)
            {
                var counter = entity.EmailSendCount + 1;

                logger.LogInformation("Updated email retry counter to {counter}", counter);
                entity.EmailSendCount = counter;
            }

            logger.LogInformation("Saving of updated data on the database");
            await dbContext.SaveChangesAsync();

            logger.LogInformation("UpdateEmailStatusAsync - End");
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating email status");
            return false;
        }
    }
}