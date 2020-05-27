using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using WebApi.BusinessLogic.Services;

namespace WebApi.BusinessLogic.Commands
{
    public class LightTurnOffCommand : ICommand
    {
        private readonly ILight light;
        private readonly ILogger<LightTurnOffCommand> logger;

        private Response? response;

        public LightTurnOffCommand(ILight light, ILogger<LightTurnOffCommand> logger)
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
                    this.response = new Response(StatusResponse.Error, message: "Rooms parameter is empty, provide rooms to turn off.");
                    logger.LogWarning($"Turning off light failed. Not rooms provided in {nameof(parameters)}.");
                    return;
                }

                light.TurnOff(parameters.ToArray());
                this.response = new Response(StatusResponse.Ok, message: $"Lights turned off. '{string.Join(", ", parameters)}' ");
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