using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.BusinessLogic;

namespace WebApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ServiceController : ControllerBase
  {
    private readonly IClientTranslator clientTranslator;
    private readonly ILogger<ServiceController> logger;

    public ServiceController(IClientTranslator clientTranslator, ILogger<ServiceController> logger)
    {
      this.clientTranslator = clientTranslator ?? throw new ArgumentNullException(nameof(clientTranslator));
      this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    public IActionResult Get(string command)
    {
        var response = this.clientTranslator.TranslateAndExecute(command);

        if (response is null || response.Status == StatusResponse.NotFound)
            return NotFound();
        
        return Ok(response);
    }
  }
}