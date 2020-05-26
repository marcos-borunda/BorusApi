using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.BusinessLogic.Commands;
using WebApi.BusinessLogic.Services;

namespace WebApi.BusinessLogic
{
    public class CommandTranslator : ICommandTranslator
    {
        public ICommand? Translate(IList<string> commandWords)
        {
            if (commandWords.Count < 2)
                return null;

            var receiver = CreateReceiver(receiver: commandWords[0], parameters: commandWords.Skip(2));

            if (receiver is null)
                return null;

            return CreateCommand(command: commandWords[1], receiver);
        }

        private object? CreateReceiver(string receiver, IEnumerable<string> parameters)
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