using System;
using System.Collections.Generic;
using WebApi.BusinessLogic.Commands;

namespace WebApi.BusinessLogic
{
    public class Invoker
    {
        public ICommand Command { get; set; }

        public Invoker(ICommand command)
        {
            Command = command ?? throw new ArgumentNullException(nameof(command));
        }

        public void Invoke(IEnumerable<string>? parameters = null)
        {
            Command.Execute(parameters);
        }
    }
}