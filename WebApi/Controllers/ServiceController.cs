using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.BusinessLogic;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly ICommandTranslator commandTranslator;
        private readonly ILogger<ServiceController> logger;

        public ServiceController(ICommandTranslator commandTranslator, ILogger<ServiceController> logger)
        {
            this.commandTranslator = commandTranslator ?? throw new ArgumentNullException(nameof(commandTranslator));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public IActionResult Get(string statement)
        {
            var commandWords = statement.Split(' ').ToList();

            if (commandWords.Count < 2)
                return BadRequest($"{nameof(statement)}: '{statement}' is not valid. It should have at least 2 words (service and command).");

            var command = this.commandTranslator.Translate(commandWords);

            if (command is null)
                return NotFound($"{nameof(statement)}: '{statement}' is not valid. Service was not found.");

            var response = new Invoker(command).Invoke();

            if (response.Status == StatusResponse.Error)
                return StatusCode(StatusCodes.Status500InternalServerError, $"{nameof(statement)}: '{statement}'. Error while executing.");

            return Ok(response.Message);
        }
    }
}