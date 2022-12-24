using GestioneSagre.Utility.Domain.UnitOfWork;
using GestioneSagre.Utility.Infrastructure.Entities;

namespace GestioneSagre.Utility.Domain.Services.Write;

public class SendEmailWriteService : ISendEmailWriteService
{
    public IUnitOfWork unitOfWork;

    public SendEmailWriteService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> CreateEmailMessage(EmailMessage request)
    {
        if (request != null)
        {
            var result = await unitOfWork.UtilityWrite.CreateAsync(request);

            if (result == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    public async Task<bool> UpdateEmailMessage(EmailMessage request)
    {
        if (request != null)
        {
            var user = await unitOfWork.UtilityRead.GetByIdAsync(request.Id);

            if (user != null)
            {
                var result = await unitOfWork.UtilityWrite.UpdateAsync(request);

                if (result == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        return false;
    }
    public async Task<bool> DeleteEmailMessage(int id, Guid emailId)
    {
        if (id != 0)
        {
            var userDetail = await unitOfWork.UtilityRead.GetByIdAsync(id);

            if (userDetail != null)
            {
                if (userDetail.Id == id && userDetail.EmailId == emailId)
                {
                    var result = await unitOfWork.UtilityWrite.DeleteAsync(userDetail);

                    if (result == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        return false;
    }
}