using System;
using BasicGamemode.Controllers;
using GamemodeDatabase;
using SampSharp.GameMode;
using SampSharp.GameMode.Controllers;

namespace BasicGamemode
{
    public class GameMode : BaseMode
    {
        protected override void OnInitialized(EventArgs e)
        {
            SetGameModeText("Basic Gamemode");
            ShowPlayerMarkers(0);
            ShowNameTags(true);
            EnableVehicleFriendlyFire();
            SetNameTagDrawDistance(110.0f);
            DisableInteriorEnterExits();

            using (var context = new GamemodeContext())
            {
                context.Database.EnsureCreated();
            }

            base.OnInitialized(e);
        }

        protected override void LoadControllers(ControllerCollection controllers)
        {
            base.LoadControllers(controllers);

            controllers.Remove<BasePlayerController>();
            controllers.Add(new PlayerController());
        }
    }
}