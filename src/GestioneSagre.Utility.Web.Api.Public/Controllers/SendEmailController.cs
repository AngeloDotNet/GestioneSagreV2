﻿using GestioneSagre.Utility.CommandStack;
using GestioneSagre.Utility.Domain.Models.InputModels;
using GestioneSagre.Utility.QueryStack;
using GestioneSagre.Utility.Web.Api.Public.Controllers.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestioneSagre.Utility.Web.Api.Public.Controllers;

public class SendEmailController : BaseController
{
    private readonly IMediator mediator;

    public SendEmailController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmailMessagesAsync()
    {
        try
        {
            var result = await mediator.Send(new GetEmailMessagesListQuery());

            if (result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmailMessageByIdAsync(int id)
    {
        try
        {
            var result = await mediator.Send(new GetEmailMessageByIdQuery(id));

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmailMessageAsync(CreateEmailMessageInputModel request)
    {
        try
        {
            var result = await mediator.Send(new CreateEmailMessageCommand(request));

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEmailMessageAsync(UpdateEmailMessageInputModel request)
    {
        try
        {
            var result = await mediator.Send(new UpdateEmailMessageCommand(request));

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEmailMessageAsync(DeleteEmailMessageInputModel request)
    {
        try
        {
            var result = await mediator.Send(new DeleteEmailMessageCommand(request));

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}