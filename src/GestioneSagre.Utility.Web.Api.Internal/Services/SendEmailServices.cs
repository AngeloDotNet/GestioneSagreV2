using AutoMapper;
using GestioneSagre.Utility.Domain.Models.ViewModels;
using GestioneSagre.Utility.Infrastructure.DataAccess;
using GestioneSagre.Utility.Infrastructure.Enum;
using Microsoft.EntityFrameworkCore;

namespace GestioneSagre.Utility.Web.Api.Internal.Services;

public class SendEmailServices : ISendEmailServices
{
    private readonly ILogger<SendEmailServices> logger;
    private readonly DataDbContext dbContext;
    private readonly IMapper mapper;

    public SendEmailServices(ILogger<SendEmailServices> logger, DataDbContext dbContext, IMapper mapper)
    {
        this.logger = logger;
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<List<EmailMessageViewModel>> GetAllEmailMessagesAsync()
    {
        var baseQuery = dbContext.EmailMessages
            .Where(x => x.Status == EmailStatus.Pending)
            .AsNoTracking();

        var dataLinq = await baseQuery.ToListAsync();
        var result = mapper.Map<List<EmailMessageViewModel>>(dataLinq);

        return result;
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