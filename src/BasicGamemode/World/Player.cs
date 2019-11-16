using BCrypt;
using GamemodeDatabase;
using GamemodeDatabase.Models;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;
using System;
using System.Linq;

namespace BasicGamemode.World
{
    /// <inheritdoc />
    /// <summary>
    ///     A child class of BasePlayer
    ///     Used to handle player custom data
    /// </summary>
    [PooledType]
    public class Player : BasePlayer
    {
        private Timer _kickTimer;
        private int _loginTries;

        /// <summary>
        ///     Fetch player data from the database using LINQ
        /// </summary>
        /// <returns>A PlayerModel object</returns>
        public PlayerModel FetchAccountData()
        {
            using (var db = new GamemodeContext())
            {
                return db.Players.FirstOrDefault(x => x.PlayerName == Name);
            }
        }

        /// <summary>
        ///     Fetch player data from the database using LINQ given a database context
        /// </summary>
        /// <param name="db">A GamemodeContext object</param>
        /// <returns>A PlayerModel object</returns>
        public PlayerModel FetchAccountData(GamemodeContext db)
        {
            return db.Players.FirstOrDefault(x => x.PlayerName == Name);
        }

        /// <summary>
        ///     Triggered when a player connect to the server
        ///     Handles the player account creation and verification
        ///     Also used for player default values
        /// </summary>
        /// <param name="e">An EventArgs object</param>
        public override void OnConnected(EventArgs e)
        {
            GameText("Test BasicGamemode", 2000, 5);

            SendClientMessage(Color.GreenYellow, $"Welcome to the server {Name}! Have fun!");

            SetWorldBounds(2500.0f, 1850.0f, 631.2963f, -454.9898f);

            ToggleSpectating(true);

            if (FetchAccountData() is null)
                RegisterPlayer();
            else
                LoginPlayer();

            base.OnConnected(e);
        }

        /// <summary>
        ///     Triggered when a player disconnect from the server
        ///     Handles cleaning up Player class and save last values to the database
        /// </summary>
        /// <param name="e">An DisconnectEventArgs object</param>
        public override void OnDisconnected(DisconnectEventArgs e)
        {
            using (var db = new GamemodeContext())
            {
                FetchAccountData(db).PositionX = Position.X;
                FetchAccountData(db).PositionY = Position.Y;
                FetchAccountData(db).PositionZ = Position.Z;
                FetchAccountData(db).FacingAngle = Angle;
                FetchAccountData(db).LastActive = DateTime.Now;

                db.SaveChanges();
            }

            base.OnDisconnected(e);
        }

        /// <summary>
        ///     Get player position as Vector3 based on X, y and Z values inside the database
        /// </summary>
        /// <returns>A Vector3 object containing player coordinates</returns>
        private Vector3 GetPlayerPositionVector3()
        {
            return new Vector3(FetchAccountData().PositionX, FetchAccountData().PositionY, FetchAccountData().PositionZ);
        }

        /// <summary>
        ///     Triggered when the _kickTimer Tick() event rises
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">an EventArgs object</param>
        private void _kickTimer_Tick(object sender, EventArgs e)
        {
            Kick();
        }

        /// <summary>
        ///     Handles the player login
        ///     Check the database for the account and log the player in
        /// </summary>
        private void LoginPlayer()
        {
            var message = $"Insert your password. Tries left: {_loginTries}/{Config.MaximumLoginTries}";
            var dialog = new InputDialog("Login", message, true, "Login", "Cancel");
            dialog.Show(this);
            dialog.Response += (sender, ev) =>
            {
                switch (ev.DialogButton)
                {
                    case DialogButton.Left:
                        {
                            if (_loginTries >= Config.MaximumLoginTries)
                            {
                                SendClientMessage(Color.OrangeRed, "You exceed maximum login tries. You have been kicked!");
                                _kickTimer = new Timer(1500, false);
                                _kickTimer.Tick += _kickTimer_Tick;
                            }
                            else if (BCryptHelper.CheckPassword(ev.InputText, FetchAccountData().Password))
                            {
                                ToggleSpectating(false);
                                SetSpawnInfo(NoTeam, 0, GetPlayerPositionVector3(), FetchAccountData().FacingAngle);
                                Spawn();
                            }
                            else
                            {
                                _loginTries++;
                                SendClientMessage(Color.Red, "Wrong password");
                                dialog.Message =
                                    $"Wrong password! Retype your password! Tries left: {_loginTries}/{Config.MaximumLoginTries}";
                                LoginPlayer();
                            }
                        }
                        break;

                    case DialogButton.Right:
                        {
                            Kick();
                        }
                        break;
                }
            };
        }

        /// <summary>
        ///     Handles the player registration
        ///     Insert a new PlayerModel record in the database
        /// </summary>
        private void RegisterPlayer()
        {
            var dialog = new InputDialog("Register", "Input your password", true, "Register", "Cancel");
            dialog.Show(this);
            dialog.Response += (sender, ev) =>
            {
                switch (ev.DialogButton)
                {
                    case DialogButton.Left:
                        {
                            using (var db = new GamemodeContext())
                            {
                                var salt = BCryptHelper.GenerateSalt(10);
                                var hash = BCryptHelper.HashPassword(ev.InputText, salt);
                                var player = new PlayerModel
                                {
                                    Password = hash,
                                    PlayerName = Name
                                };
                                db.Players.Add(player);
                                db.SaveChanges();

                                LoginPlayer();
                            }
                        }
                        break;

                    case DialogButton.Right:
                        {
                            Kick();
                        }
                        break;
                }
            };
        }
    }
}