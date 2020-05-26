using System.Collections.Generic;
using WebApi.BusinessLogic.Commands;

namespace WebApi.BusinessLogic
{
    public interface ICommandTranslator
    {
        ICommand? Translate(string service, string action, IEnumerable<string>? parameters = null);
    }
}