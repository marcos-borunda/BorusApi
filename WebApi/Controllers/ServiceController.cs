using System;
using System.Collections.Generic;
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

        [HttpGet("{service}/{command}")]
        public IActionResult Get(string service, string command, [FromQuery] IEnumerable<string>? parameters)
        {
            var commandInstance = this.commandTranslator.Translate(service, command, parameters);

            if (commandInstance is null)
            {
                var message = $"Service '{service}/{command}' was not found.";
                this.logger.LogWarning(message);
                return NotFound(message);
            }

            var response = new Invoker(commandInstance).Invoke(parameters);

            if (response.Status == StatusResponse.Error)
            {
                this.logger.LogError(response.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
            }

            return Ok(response.Message);
        }
    }
}