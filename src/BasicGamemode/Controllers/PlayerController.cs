using BasicGamemode.World;
using SampSharp.GameMode.Controllers;

namespace BasicGamemode.Controllers
{
    /// <summary>
    ///     Share BasePlayer events with BasePlayer child
    /// </summary>
    public class PlayerController : BasePlayerController
    {
        /// <summary>
        ///     Register the BasePlayer childs types
        /// </summary>
        public override void RegisterTypes()
        {
            Player.Register<Player>();
        }
    }
}