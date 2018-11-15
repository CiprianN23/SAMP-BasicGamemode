using System;
using BasicGamemode.Controllers;
using SampSharp.GameMode;
using SampSharp.GameMode.Controllers;

namespace BasicGamemode
{
    public class GameMode : BaseMode
    {
        protected override void OnInitialized(EventArgs e)
        {
            SetGameModeText("Test RPG");
            ShowPlayerMarkers(0);
            ShowNameTags(true);
            EnableVehicleFriendlyFire();
            SetNameTagDrawDistance(110.0f);
            DisableInteriorEnterExits();

            

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