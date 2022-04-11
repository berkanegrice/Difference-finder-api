using System;
using System.Threading.Tasks;
using Diff.Application.Commands;
using Diff.Application.Models;
using Diff.Infrastructure.Files;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;


namespace Diff.Web.Controllers
{
    [Route("diff/{id:int}")]
    [ApiController]
    public class DifferenceController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DifferenceController> _logger;
        
        public DifferenceController(IMediator mediator, ILogger<DifferenceController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Route("left")]
        public async Task<ActionResult<bool>> Left([FromBody] InputVm command)
        {
            _logger.LogInformation($"Left method is called at {DateTime.UtcNow.ToLongTimeString()}");

            if (!int.TryParse((string)RouteData.Values["id"], out var id))
                return UnprocessableEntity();

            var result = await _mediator.Send(
                new AddInputCommand() { Id = id, Side = "left", Base64Str = command.Base64Str }
            );
            return Ok(result);
        }

        [HttpPost]
        [Route("right")]
        public async Task<ActionResult<bool>> Right([FromBody] InputVm command)
        {
            _logger.LogInformation($"Right method is called at {DateTime.UtcNow.ToLongTimeString()}");

            if (!int.TryParse((string)RouteData.Values["id"], out var id))
                return UnprocessableEntity();

            var result = await _mediator.Send(
                new AddInputCommand() { Id = id, Side = "right", Base64Str = command.Base64Str }
            );
            return Ok(result);
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<ResultVm>> GetDiff(int id)
        {
            _logger.LogInformation($"GetDiff method is called at {DateTime.UtcNow.ToLongTimeString()}");
            var result = await _mediator.Send(
                new GetDifferencesCommand() { Id = id }
            );
            return result;
        }
    }
}