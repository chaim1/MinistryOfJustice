using CourtManagement.API.Models.Responses;
using CourtManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CourtManagement.API.Exceptions;

namespace CourtManagement.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly ILogger _logger;
        protected readonly IMessageBusService _messageBus;

        protected BaseController(ILogger logger, IMessageBusService messageBus)
        {
            _logger = logger;
            _messageBus = messageBus;
        }

        protected async Task<ActionResult<T>> HandleOperation<T>(Func<Task<T>> operation)
        {
            try
            {
                var result = await operation();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return HandleError(ex);
            }
        }

        private ActionResult HandleError(Exception ex)
        {
            return ex switch
            {
                NotFoundException => NotFound(new ErrorResponse(ex.Message)),
                ValidationException => BadRequest(new ErrorResponse(ex.Message)),
                _ => StatusCode(500, new ErrorResponse("Internal server error"))
            };
        }
    }
}