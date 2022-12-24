using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace GestioneSagre.Utility.Web.Api.Public.Controllers.Common;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class BaseController : ControllerBase
{

}