using System.Collections.Generic;

namespace WebApi.BusinessLogic.Services
{
    public class Light : ILight
    {
        public void TurnOn(params string[] rooms)
        {
            foreach(var room in rooms)
            {
                // Turns light on
            }
        }

        public void TurnOff(params string[] rooms)
        {
            foreach(var room in rooms)
            {
                // Turns light off
            }
        }
    }
}