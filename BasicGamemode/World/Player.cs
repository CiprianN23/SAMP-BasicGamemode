using System;
using System.Linq;
using GamemodeDatabase;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.World;
using SampSharp.GameMode.SAMP;
using GamemodeDatabase.Models;
using Microsoft.EntityFrameworkCore;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;

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
                    return db.Players.Where(x => x.PlayerName == this.Name).SingleOrDefault();
                }
            }
        }


        public override void OnConnected(EventArgs e)
        {
            GameText("Test RPG", 2000, 5);

            SendClientMessage(Color.GreenYellow, $"Welcome to the server {this.Name}! Have fun!");

            SetWorldBounds(2500.0f, 1850.0f, 631.2963f, -454.9898f);

            try
            {
                if (Account is null)
                {
                    var dialog = new InputDialog("Register", "Input your password", true, "Accept", "Cancel");
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
                                        PlayerName = this.Name
                                    };
                                    db.Players.Add(player);
                                    db.SaveChanges();
                                }
                            }
                                break;
                            case DialogButton.Right:
                            {
                                this.Kick();
                            }
                                break;
                        }
                    };

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            

            base.OnConnected(e);
        }
    }
}