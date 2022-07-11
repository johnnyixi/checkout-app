using System.Net.Mime;
using CheckoutApp.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutApp.Controllers;

[ApiController]
[Route("[controller]")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDetails))]
public class BaseController : ControllerBase
{
}
