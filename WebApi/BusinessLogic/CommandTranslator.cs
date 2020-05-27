using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using WebApi.BusinessLogic.Commands;
using WebApi.BusinessLogic.Services;

namespace WebApi.BusinessLogic
{
    public class CommandTranslator : ICommandTranslator
    {
        private readonly ILoggerFactory loggerFactory;

        public CommandTranslator(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public ICommand? Translate(string service, string command, IEnumerable<string>? parameters = null)
        {
            if (service is null)
                throw new ArgumentNullException(nameof(service));

            if (command is null)
                throw new ArgumentNullException(nameof(command));

            var receiver = CreateReceiver(receiver: service.ToLower(), parameters);

            if (receiver is null)
                return null;

            return CreateCommand(command.ToLower(), receiver);
        }

        private object? CreateReceiver(string receiver, IEnumerable<string>? parameters)
            => receiver switch
            {
                "movistar" => new Movistar(),
                "light" => new Light(),
                _ => null
            };

        private ICommand? CreateCommand(string command, object receiver)
            => command switch
            {
                "datos" => new MovistarDataUsageCommand((IMovistar)receiver, this.loggerFactory.CreateLogger<MovistarDataUsageCommand>()),
                "on" => new LightTurnOnCommand((ILight)receiver, this.loggerFactory.CreateLogger<LightTurnOnCommand>()),
                "off" => new LightTurnOffCommand((ILight)receiver, this.loggerFactory.CreateLogger<LightTurnOffCommand>()),
                _ => null
            };
    }
}