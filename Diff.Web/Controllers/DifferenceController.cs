using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Diff.Application.Commands;
using Diff.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#pragma warning disable CS1591


namespace Diff.Web.Controllers
{
    /// <summary>
    /// this controller summary command.
    /// </summary>
    [ApiController]
    [Route("/v{version:apiVersion}/diff/{id:int}")]
    [ApiVersion("1.0")]
    
    public class DifferenceController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DifferenceController> _logger;

        public DifferenceController(IMediator mediator, ILogger<DifferenceController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// This method adds the new pair to left.
        /// </summary>
        /// <param name="command"> the new pair. </param>
        /// <returns></returns>
        [HttpPost]
        [Route("left")]
        public async Task<ActionResult<bool>> Left([Required] [FromBody] InputVm command)
        {
            _logger.LogInformation($"Left method is called at {DateTime.UtcNow.ToLongTimeString()}");

            if (!int.TryParse((string)RouteData.Values["id"], out var id))
                return UnprocessableEntity();

            var result = await _mediator.Send(
                new AddInputCommand() { Id = id, Side = "left", Data = command.Data }
            );
            return Ok(result);
        }

        /// <summary>
        /// This methods adds the new pair to right.
        /// </summary>
        /// <param name="command"> the new pair. </param>
        /// <returns></returns>
        [HttpPost]
        [Route("right")]
        public async Task<ActionResult<bool>> Right([Required] [FromBody] InputVm command)
        {
            _logger.LogInformation($"Right method is called at {DateTime.UtcNow.ToLongTimeString()}");

            if (!int.TryParse((string)RouteData.Values["id"], out var id))
                return UnprocessableEntity();

            var result = await _mediator.Send(
                new AddInputCommand() { Id = id, Side = "right", Data = command.Data }
            );
            return Ok(result);
        }

        /// <summary>
        /// This methods produces the differences between inserted pairs from left to right.
        /// </summary>
        /// <param name="id"> the pair id. </param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<ResultVm>> GetDiff([Required] int id)
        {
            _logger.LogInformation($"GetDiff method is called at {DateTime.UtcNow.ToLongTimeString()}");
            var result = await _mediator.Send(
                new GetDifferencesCommand() { Id = id }
            );
            return result;
        }
    }
}