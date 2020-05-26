using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using WebApi.BusinessLogic.Services;

namespace WebApi.BusinessLogic.Commands
{
    public class MovistarDataUsageCommand : ICommand
    {
        private readonly IMovistar movistar;
        //private readonly ILogger<MovistarDataUsageCommand> logger;

        private Response? response;

        public MovistarDataUsageCommand(IMovistar movistar)//, ILogger<MovistarDataUsageCommand> logger)
        {
            this.movistar = movistar ?? throw new ArgumentNullException(nameof(movistar));
            //this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Response? Response => response;

        public void Execute(IEnumerable<string>? parameters = null)
        {
            try
            {
                this.response = new Response(StatusResponse.Ok, message: movistar.GetDataUsage());
            }
            catch(Exception)
            {
                var errorMessage = "Error while getting Movistar's data usage.";
                //logger.LogError(ex, errorMessage);
                this.response = new Response(StatusResponse.Error, errorMessage);
            }
        }

    }
}