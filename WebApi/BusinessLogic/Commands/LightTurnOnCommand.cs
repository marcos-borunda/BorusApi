using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using WebApi.BusinessLogic.Services;

namespace WebApi.BusinessLogic.Commands
{
    public class LightTurnOnCommand : ICommand
    {
        private readonly ILight light;
        private readonly ILogger<LightTurnOnCommand> logger;

        private Response? response;

        public LightTurnOnCommand(ILight light, ILogger<LightTurnOnCommand> logger)
        {
            this.light = light ?? throw new ArgumentNullException(nameof(light));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Response? Response => response;

        public void Execute(IEnumerable<string>? parameters = null)
        {
            try
            {
                if (parameters == null || !parameters.Any())
                {
                    this.response = new Response(StatusResponse.Error, message: "Rooms parameter is empty, provide rooms to turn on.");
                    logger.LogWarning($"Turning on light failed. Not rooms provided in {nameof(parameters)}.");
                    return;
                }

                light.TurnOn(parameters.ToArray());
                this.response = new Response(StatusResponse.Ok, message: $"Lights turned on. '{string.Join(", ", parameters)}' ");
            }
            catch(Exception ex)
            {
                var errorMessage = "Error while turning .";
                logger.LogError(ex, errorMessage);
                this.response = new Response(StatusResponse.Error, errorMessage);
            }
        }

    }
}