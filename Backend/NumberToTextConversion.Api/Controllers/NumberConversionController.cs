using Microsoft.AspNetCore.Mvc;
using NumberConversionApi.Interfaces;
namespace NumberConversionApi.Controllers;

[ApiController]
[Route("[controller]")]
public class NumberConversionController(INumberConversionService service) : ControllerBase
{
    private readonly INumberConversionService _service = service;

    [HttpGet("{value}")]
    public ActionResult<string> Get([FromRoute] string value)
    {
        try
        {
            var totalAmountInText = _service.Convert(value);
            return Ok(totalAmountInText);
        }
        catch (ArgumentException ex)
        {
            return Problem(detail: ex.Message, statusCode: 400);
        }
    }
}
