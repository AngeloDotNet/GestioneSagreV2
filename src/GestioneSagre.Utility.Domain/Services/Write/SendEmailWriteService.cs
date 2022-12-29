using GestioneSagre.Utility.Domain.UnitOfWork;
using GestioneSagre.Utility.Infrastructure.Entities;
using Microsoft.Extensions.Logging;

namespace GestioneSagre.Utility.Domain.Services.Write;

public class SendEmailWriteService : ISendEmailWriteService
{
    public IUnitOfWork unitOfWork;
    private readonly ILogger logger;

    public SendEmailWriteService(IUnitOfWork unitOfWork, ILogger<SendEmailWriteService> logger)
    {
        this.unitOfWork = unitOfWork;
        this.logger = logger;
    }

    public async Task<bool> CreateEmailMessage(EmailMessage request)
    {
        try
        {
            if (request != null)
            {
                logger.LogInformation("Creating a new record for email {email}", request.EmailId);
                var result = await unitOfWork.UtilityWrite.CreateAsync(request);

                if (result == true)
                {
                    logger.LogInformation("Successfully created a new record for email {email}", request.EmailId);
                    return true;
                }
                else
                {
                    logger.LogWarning("Error creating new record for email {email}", request.EmailId);
                    return false;
                }
            }

            logger.LogWarning("Failed to create new record for email {email} due to incomplete data", request.EmailId);
            return false;
        }
        catch (Exception exc)
        {
            logger.LogError(exc, "Error creating new record for email {email}", request.EmailId);
            return false;
        }
    }

    public async Task<bool> UpdateEmailMessage(EmailMessage request)
    {
        try
        {
            if (request != null)
            {
                logger.LogInformation("Email detail search with id {id}", request.Id);
                var user = await unitOfWork.UtilityRead.GetByIdAsync(request.Id);

                if (user != null)
                {
                    logger.LogInformation("Updating data for email {email}", request.EmailId);
                    var result = await unitOfWork.UtilityWrite.UpdateAsync(request);

                    if (result == true)
                    {
                        logger.LogInformation("Successfully updating data for email {email}", request.EmailId);
                        return true;
                    }
                    else
                    {
                        logger.LogWarning("Error updating data for email {email}", request.EmailId);
                        return false;
                    }
                }
            }

            logger.LogWarning("Failed to update data for email {email}", request.EmailId);
            return false;
        }
        catch (Exception exc)
        {
            logger.LogError(exc, "Error updating data for email {email}", request.EmailId);
            return false;
        }
    }

    public async Task<bool> DeleteEmailMessage(int id, Guid emailId)
    {
        try
        {
            if (id != 0)
            {
                logger.LogInformation("Email detail search with id {id}", id);
                var userDetail = await unitOfWork.UtilityRead.GetByIdAsync(id);

                if (userDetail != null)
                {
                    if (userDetail.Id == id && userDetail.EmailId == emailId)
                    {
                        logger.LogInformation("Deleting data for emailId {emailId}", emailId);
                        var result = await unitOfWork.UtilityWrite.DeleteAsync(userDetail);

                        if (result == true)
                        {
                            logger.LogInformation("Successfully deleting data for emailId {emailId}", emailId);
                            return true;
                        }
                        else
                        {
                            logger.LogWarning("Error deleting data for emailId {emailId}", emailId);
                            return false;
                        }
                    }
                }
                else
                {
                    logger.LogWarning("Failed to delete data for emailId {emailId}", emailId);
                    return false;
                }
            }

            logger.LogWarning("Failed to delete data for emailId {emailId}", emailId);
            return false;
        }
        catch (Exception exc)
        {
            logger.LogError(exc, "Error deleting data for emailId {emailId}", emailId);
            return false;
        }
    }
}