using System;
using BasicGamemode.Controllers;
using GamemodeDatabase;
using SampSharp.GameMode;
using SampSharp.GameMode.Controllers;

namespace BasicGamemode
{
    /// <summary>
    /// The main GameMode class
    /// Handles the GameMode startup
    /// </summary>
    public class GameMode : BaseMode
    {
        /// <summary>
        /// Triggered when the GameMode starts
        /// Used to setup initial values on the server startup and ensure database creation
        /// </summary>
        /// <param name="e">An EventArgs object</param>
        protected override void OnInitialized(EventArgs e)
        {
            SetGameModeText("Basic Gamemode");
            ShowPlayerMarkers(0);
            ShowNameTags(true);
            EnableVehicleFriendlyFire();
            SetNameTagDrawDistance(110.0f);
            DisableInteriorEnterExits();

            try
            {
                using (var context = new GamemodeContext())
                {
                    context.Database.EnsureCreated();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Connection to the database failed. Check database login details or if database is running.");
                Console.WriteLine(exception);
                throw;
            }
            

            base.OnInitialized(e);
        }

        /// <summary>
        /// Handles the creation of controllers on the GameMode startup
        /// </summary>
        /// <param name="controllers">A controllerCollection object</param>
        protected override void LoadControllers(ControllerCollection controllers)
        {
            base.LoadControllers(controllers);

            controllers.Remove<BasePlayerController>();
            controllers.Add(new PlayerController());
        }
    }
}