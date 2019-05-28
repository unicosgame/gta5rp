using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

// https://www.youtube.com/watch?v=drHqAE4sITg

namespace RPClient
{
    public class WeaponSpawner : BaseScript
    {
        public WeaponSpawner()
        {
            // "giveweapon" -> command name
            // src -> source player ID
            // args -> command arguments
            // raw -> raw command
            API.RegisterCommand("giveweapon", new Action<int, List<object>, string>((src, args, raw) =>
            {
                var argList = args.Select(o => o.ToString()).ToList();
                if (argList.Any() && Enum.TryParse(argList[0], true, out WeaponHash weapon))
                {
                    Game.PlayerPed.Weapons.Give(weapon, 999, true, true);
                }
            }), false);
        }

        public static void SendChatMessage(string title, string message, int r, int g, int b)
        {
            var msg = new Dictionary<string, object>
            {
                ["color"] = new[] { r, g, b },
                ["args"] = new[] { title, message }
            };

            TriggerEvent("chat:addMessage", msg);
        }
    }
}
