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
        var baseQuery = dbContext.EmailMessages
            .Where(x => x.Status == EmailStatus.Pending)
            .AsNoTracking();

        var messages = await baseQuery
            .Select(email => EmailMessageViewModel.FromEntity(email))
            .ToListAsync();

        return messages;
    }

    public async Task<EmailMessageViewModel> GetEmailMessageAsync(Guid emailId)
    {
        var baseQuery = dbContext.EmailMessages
            .AsNoTracking()
            .Where(x => x.EmailId == emailId)
            .Select(email => EmailMessageViewModel.FromEntity(email));

        var message = await baseQuery.FirstOrDefaultAsync();

        return message;
    }

    public async Task<bool> UpdateEmailStatusAsync(int id, Guid emailId, int status)
    {
        try
        {
            var entity = await dbContext.EmailMessages.FindAsync(id);

            if (entity == null || entity.EmailId != emailId)
            {
                return false;
            }

            if (status == 0)
            {
                entity.Status = EmailStatus.Pending;
            }

            if (status == 1)
            {
                entity.EffectiveSendDate = DateTime.Now;
                entity.Status = EmailStatus.Sent;
            }

            if (status == 2)
            {
                entity.Status = EmailStatus.Failed;
            }

            if (status == 3)
            {
                var counter = entity.EmailSendCount + 1;
                entity.EmailSendCount = counter;
            }

            await dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating email status");
            return false;
        }
    }
}