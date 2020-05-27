using System.Collections.Generic;
using WebApi.BusinessLogic.Commands;

namespace WebApi.BusinessLogic
{
    public interface ICommandTranslator
    {
        ICommand? Translate(string service, string command, IEnumerable<string>? parameters = null);
    }
}