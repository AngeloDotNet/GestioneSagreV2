﻿using GestioneSagre.Utility.Domain.Models.ViewModels;

namespace GestioneSagre.Utility.Core;

public interface ISendEmailServices
{
    Task<List<EmailMessageViewModel>> GetAllEmailMessagesAsync();
    Task<EmailMessageViewModel> GetEmailMessageAsync(Guid emailId);
    Task<bool> UpdateEmailStatusAsync(int id, Guid emailId, int status);
}