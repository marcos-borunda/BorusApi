namespace WebApi.BusinessLogic.Services
{
    public interface ILight
    {
        void TurnOn(params string[] rooms);
        void TurnOff(params string[] rooms);
    }
}