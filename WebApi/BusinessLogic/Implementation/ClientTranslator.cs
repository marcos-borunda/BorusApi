using System;
using WebApi.BusinessLogic.Commands;
using WebApi.BusinessLogic.Services;

namespace WebApi.BusinessLogic.Implementation
{
    public class ClientTranslator : IClientTranslator
    {
        public Response TranslateAndExecute(string words)
        {
            var listOfWords = words.Split(' ');

            if (listOfWords.Length < 2)
                return new Response(StatusResponse.NotFound);

            var receiver = CreateReceiver(listOfWords[0], listOfWords);

            if (receiver is null)
                return new Response(StatusResponse.NotFound);

            var command = CreateCommand(listOfWords[1], receiver);

            if (command is null)
                return new Response(StatusResponse.NotFound);

            var invoker = new Invoker(command);
            
            invoker.Invoke();

            return invoker.Command.Response ?? new Response(StatusResponse.Error, "Response was null.");
        }

        private object? CreateReceiver(string receiver, string[] parameters)
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