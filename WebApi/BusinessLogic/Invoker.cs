using System;
using System.Collections.Generic;
using WebApi.BusinessLogic.Commands;

namespace WebApi.BusinessLogic
{
    public class Invoker : IInvoker
    {
        private readonly ICommand command;

        public Invoker(ICommand command)
        {
            this.command = command ?? throw new ArgumentNullException(nameof(command));
        }

        public Response Invoke(IEnumerable<string>? parameters = null)
        {
            this.command.Execute(parameters);
            return this.command.Response ?? new Response(StatusResponse.Error, "Error while invoking command.");
        }
    }
}