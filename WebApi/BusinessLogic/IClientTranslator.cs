namespace WebApi.BusinessLogic
{
    public interface IClientTranslator
    {
        Response TranslateAndExecute(string words);
    }
}