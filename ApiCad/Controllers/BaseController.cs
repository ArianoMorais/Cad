using Microsoft.AspNetCore.Mvc;
using UserModule.Domain.Domain.Exceptions;

namespace ApiCad.Controllers
{
    [ApiController]
    public abstract class BaseController<T> : ControllerBase
    {
        protected readonly ILogger<T> _logger;

        protected BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }

        protected ActionResult HandleError(Exception ex, string message)
        {
            _logger.LogError(ex, message);
            return StatusCode(500, "An error occurred. Please try again later.");
        }

        protected IActionResult HandleValidationException(CadException ex)
        {
            return BadRequest(new { Errors = ex.Errors, Message = ex.Message });
        }

        protected ActionResult ValidateEntity<TModel>(TModel entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return null;
        }

        protected ActionResult HandleNotFound(object result)
        {
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
