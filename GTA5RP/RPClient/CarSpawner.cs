using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

// https://docs.fivem.net/scripting-manual/introduction/creating-your-first-script-csharp/

namespace RPClient
{
    class CarSpawner : BaseScript
    {
        public CarSpawner()
        {
            API.RegisterCommand("car", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                // account for the argument not being passed
                var model = "adder";
                if (args.Count > 0)
                {
                    model = args[0].ToString();
                }

                // check if the model actually exists
                // assumes the directive `using static CitizenFX.Core.Native.API;`
                var hash = (uint)API.GetHashKey(model);
                if (!API.IsModelInCdimage(hash) || !API.IsModelAVehicle(hash))
                {
                    TriggerEvent("chat:addMessage", new
                    {
                        color = new[] { 255, 0, 0 },
                        args = new[] { "[CarSpawner]", $"It might have been a good thing that you tried to spawn a {model}. Who even wants their spawning to actually ^*succeed?" }
                    });
                    return;
                }

                // create the vehicle
                var vehicle = await World.CreateVehicle(model, Game.PlayerPed.Position, Game.PlayerPed.Heading);

                // set the player ped into the vehicle and driver seat
                Game.PlayerPed.SetIntoVehicle(vehicle, VehicleSeat.Driver);

                // tell the player
                TriggerEvent("chat:addMessage", new
                {
                    color = new[] { 255, 0, 0 },
                    args = new[] { "[CarSpawner]", $"Woohoo! Enjoy your new ^*{model}!" }
                });
            }), false);
        }
    }
}
