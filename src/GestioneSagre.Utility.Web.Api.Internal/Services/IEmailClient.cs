using Microsoft.AspNetCore.Identity.UI.Services;

namespace GestioneSagre.Utility.Web.Api.Internal.Services;

public interface IEmailClient : IEmailSender
{
    Task<bool> SendEmailAsync(string recipientEmail, string replyToEmail, string subject, string htmlMessage, CancellationToken token = default);
}