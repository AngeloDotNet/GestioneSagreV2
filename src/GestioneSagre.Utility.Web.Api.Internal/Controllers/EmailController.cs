using GestioneSagre.Shared.Models.InputModels;
using GestioneSagre.Utility.Web.Api.Internal.Controllers.Common;
using GestioneSagre.Utility.Web.Api.Internal.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestioneSagre.Utility.Web.Api.Internal.Controllers;

public class EmailController : BaseController
{
    private readonly ILogger logger;
    private readonly IEmailClient emailClient;

    public EmailController(ILogger<EmailController> logger, IEmailClient emailClient)
    {
        this.logger = logger;
        this.emailClient = emailClient;
    }

    [HttpPost]
    public async Task<IActionResult> SendEmailMessageAsync(EmailInputModel request)
    {
        try
        {
            if (!string.IsNullOrEmpty(request.ReplyEmail))
            {
                var result = await emailClient.SendEmailAsync(request.RecipientEmail, request.ReplyEmail, request.Subject, request.Message);

                if (!result)
                {
                    return BadRequest();
                }
            }
            else
            {
                var result = await emailClient.SendEmailAsync(request.RecipientEmail, null, request.Subject, request.Message);

                if (!result)
                {
                    return BadRequest();
                }
            }

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}