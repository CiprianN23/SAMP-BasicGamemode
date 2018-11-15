using BasicGamemode.World;
using SampSharp.GameMode.Controllers;

namespace BasicGamemode.Controllers
{
    public class PlayerController : BasePlayerController
    {
        public override void RegisterTypes()
        {
            Player.Register<Player>();
        }
    }
}