using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.BusinessLogic.Commands;
using WebApi.BusinessLogic.Services;

namespace WebApi.BusinessLogic
{
    public class CommandTranslator : ICommandTranslator
    {
        public ICommand? Translate(string service, string action, IEnumerable<string>? parameters = null)
        {
            var receiver = CreateReceiver(receiver: service, parameters);

            if (receiver is null)
                return null;

            return CreateCommand(command: action, receiver);
        }

        private object? CreateReceiver(string receiver, IEnumerable<string>? parameters)
            => receiver switch
            {
                "movistar" => new Movistar(),
                _ => null
            };
        
        private ICommand? CreateCommand(string command, object receiver)
            => command switch
            {
                "datos" => new MovistarDataUsageCommand((IMovistar) receiver),
                _ => null
            };
    }
}