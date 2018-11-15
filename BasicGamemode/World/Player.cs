using System;
using System.Linq;
using GamemodeDatabase;
using GamemodeDatabase.Models;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;

namespace BasicGamemode.World
{
    [PooledType]
    public class Player : BasePlayer
    {
        public PlayerModel Account
        {
            get
            {
                using (var db = new GamemodeContext())
                {
                    return db.Players.Where(x => x.PlayerName == Name).SingleOrDefault();
                }
            }
        }


        public override void OnConnected(EventArgs e)
        {
            GameText("Test RPG", 2000, 5);

            SendClientMessage(Color.GreenYellow, $"Welcome to the server {Name}! Have fun!");

            SetWorldBounds(2500.0f, 1850.0f, 631.2963f, -454.9898f);


            if (Account is null)
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
                                var player = new PlayerModel
                                {
                                    Password = ev.InputText,
                                    PlayerName = Name
                                };
                                db.Players.Add(player);
                                db.SaveChanges();
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
            else
            {
                var dialog = new InputDialog("Login", "Insert your password", true, "Login", "Cancel");
                dialog.Show(this);
                dialog.Response += (sender, ev) =>
                {
                    switch (ev.DialogButton)
                    {
                        case DialogButton.Left:
                        {
                            using (var db = new GamemodeContext())
                            {
                                var player = Account;
                                if (ev.InputText == player.Password)
                                {
                                    SetSpawnInfo(NoTeam, 0, new Vector3(1.0, 1.0, 1.5), 90.0f);
                                    Spawn();
                                }
                                else
                                {
                                    SendClientMessage(Color.Red, "Wrong password");
                                }
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


            base.OnConnected(e);
        }
    }
}