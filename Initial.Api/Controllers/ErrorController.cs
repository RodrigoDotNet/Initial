using Initial.Api.Resources;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Initial.Api.Controllers
{
    // Ref: @ErrorHandler

    /// <summary>
    /// Tratamento de erro
    /// </summary>
    [ApiController]
    [ApiVersionNeutral]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("/error-local-development")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ErrorLocalDevelopment(
            [FromServices] IHostingEnvironment webHostEnvironment)
        {
            if (!webHostEnvironment.IsDevelopment())
            {
                throw new InvalidOperationException();
            }

            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            var ex = feature?.Error;

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Instance = feature?.Path,
                Title = ex != null
                    ? $"{ex.GetType().Name}: {ex.Message}"
                    : Messages.Error_Error,
                Detail = ex?.StackTrace
            };

            return StatusCode(problemDetails.Status.Value, problemDetails);
        }

        [Route("/error")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Error(
            [FromServices] IHostingEnvironment webHostEnvironment)
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            var ex = feature?.Error;

            var isDev = webHostEnvironment.IsDevelopment();

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Instance = feature?.Path,
                Title = isDev && ex != null
                    ? $"{ex.GetType().Name}: {ex.Message}"
                    : Messages.Error_Error,
                Detail = isDev ? ex?.StackTrace : null
            };

            return StatusCode(problemDetails.Status.Value, problemDetails);
        }
    }
}
