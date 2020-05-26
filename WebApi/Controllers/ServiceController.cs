using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.BusinessLogic;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly ICommandTranslator commandTranslator;
        private readonly ILogger<ServiceController> logger;

        public ServiceController(ICommandTranslator commandTranslator, ILogger<ServiceController> logger)
        {
            this.commandTranslator = commandTranslator ?? throw new ArgumentNullException(nameof(commandTranslator));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{service}/{method}")]
        public IActionResult Get(string service, string method, [FromQuery] IEnumerable<string>? parameters)
        {
            var command = this.commandTranslator.Translate(service, method, parameters);

            if (command is null)
                return NotFound($"Service '{service}/{method}' was not found.");

            var response = new Invoker(command).Invoke();

            if (response.Status == StatusResponse.Error)
                return StatusCode(StatusCodes.Status500InternalServerError, $"{nameof(service)}: '{service}'. Error while executing.");

            return Ok(response.Message);
        }
    }
}