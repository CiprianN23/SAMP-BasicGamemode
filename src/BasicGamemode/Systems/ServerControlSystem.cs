using SampSharp.Entities;
using SampSharp.Entities.SAMP;

namespace BasicGamemode.Systems;

public class ServerControlSystem : ISystem
{
    [Event]
    public void OnGameModeInit(IServerService serverService)
    {
        serverService.SetGameModeText("Basic Gamemode");
        serverService.SendRconCommand("language English");
        serverService.ShowPlayerMarkers(PlayerMarkersMode.Off);
        serverService.ShowNameTags(true);
        serverService.EnableVehicleFriendlyFire();
        serverService.EnableStuntBonus(false);
        serverService.SetNameTagDrawDistance(110.0f);
        serverService.DisableInteriorEnterExits();
    }
}

