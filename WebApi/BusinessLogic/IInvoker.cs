using System.Collections.Generic;

namespace WebApi.BusinessLogic
{
    public interface IInvoker
    {
        Response Invoke(IEnumerable<string>? parameters = null);
    }
}