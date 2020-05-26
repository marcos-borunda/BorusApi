using System.Collections.Generic;

namespace WebApi.BusinessLogic.Commands
{
    public interface ICommand
    {
        Response? Response { get; }
        void Execute(IEnumerable<string>? parameters = null);
    }
}