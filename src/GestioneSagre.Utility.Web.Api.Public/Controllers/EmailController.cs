using GestioneSagre.Utility.CommandStack;
using GestioneSagre.Utility.Domain.Models.InputModels;
using GestioneSagre.Utility.QueryStack;
using GestioneSagre.Utility.Web.Api.Public.Controllers.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestioneSagre.Utility.Web.Api.Public.Controllers;

public class EmailController : BaseController
{
    private readonly IMediator mediator;
    private readonly ILogger logger;

    public EmailController(IMediator mediator, ILogger<EmailController> logger)
    {
        this.mediator = mediator;
        this.logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmailMessagesAsync()
    {
        try
        {
            logger.LogInformation("Method invocation GetEmailMessagesAsync");
            var result = await mediator.Send(new GetEmailMessagesListQuery());

            if (result.Count == 0)
            {
                logger.LogWarning("No email messages found");
                return NotFound();
            }

            logger.LogInformation("Email messages found");
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Method error GetEmailMessagesAsync");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmailMessageByIdAsync(int id)
    {
        try
        {
            logger.LogInformation("Method invocation GetEmailMessageByIdAsync {id}", id);
            var result = await mediator.Send(new GetEmailMessageByIdQuery(id));

            if (result == null)
            {
                logger.LogWarning("No email message found");
                return NotFound();
            }

            logger.LogInformation("Email message found");
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Method error GetEmailMessageByIdAsync {id}", id);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmailMessageAsync(CreateEmailMessageInputModel request)
    {
        try
        {
            logger.LogInformation("Method invocation CreateEmailMessageAsync {emailId}", request.EmailId);
            var result = await mediator.Send(new CreateEmailMessageCommand(request));

            if (!result)
            {
                logger.LogWarning("Email message not created");
                return BadRequest();
            }

            logger.LogInformation("Email message created");
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Method error CreateEmailMessageAsync {emailId}", request.EmailId);
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEmailMessageAsync(UpdateEmailMessageInputModel request)
    {
        try
        {
            logger.LogInformation("Method invocation UpdateEmailMessageAsync {emailId}", request.EmailId);
            var result = await mediator.Send(new UpdateEmailMessageCommand(request));

            if (!result)
            {
                logger.LogWarning("Email message not updated");
                return BadRequest();
            }

            logger.LogInformation("Email message updated");
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Method error UpdateEmailMessageAsync {emailId}", request.EmailId);
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEmailMessageAsync(DeleteEmailMessageInputModel request)
    {
        try
        {
            logger.LogInformation("Method invocation DeleteEmailMessageAsync {emailId}", request.EmailId);
            var result = await mediator.Send(new DeleteEmailMessageCommand(request));

            if (!result)
            {
                logger.LogWarning("Email message not deleted");
                return BadRequest();
            }

            logger.LogInformation("Email message deleted");
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Method error DeleteEmailMessageAsync {emailId}", request.EmailId);
            return BadRequest(ex.Message);
        }
    }
}