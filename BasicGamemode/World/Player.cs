﻿using System;
using System.Linq;
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

namespace BasicGamemode.World
{
    [PooledType]
    public class Player : BasePlayer
    {
        private Timer _kickTimer;
        private int _loginTries;

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
                RegisterPlayer();
            else
                LoginPlayer();


            base.OnConnected(e);
        }

        public override void OnDisconnected(DisconnectEventArgs e)
        {
            base.OnDisconnected(e);

            var player = Account;
            player.PositionX = Position.X;
            player.PositionY = Position.Y;
            player.PositionZ = Position.Z;
            player.FacingAngle = Angle;
            
            UpdatePlayerData(player);
        }

        private Vector3 GetPlayerPositionVector3()
        {
            var player = Account;
            return new Vector3(player.PositionX, player.PositionY, player.PositionZ);
        }

        private void UpdatePlayerData(PlayerModel player)
        {
            using (var db = new GamemodeContext())
            {
                db.Players.Update(player);
                db.SaveChanges();
            }
        }

        private void _kickTimer_Tick(object sender, EventArgs e)
        {
            Kick();
        }

        private void LoginPlayer()
        {
            var dialog = new InputDialog("Login", "Insert your password", true, "Login", "Cancel");
            dialog.Show(this);
            dialog.Response += (sender, ev) =>
            {
                switch (ev.DialogButton)
                {
                    case DialogButton.Left:
                    {
                        var player = Account;

                        if (_loginTries >= Config.MaximumLoginTries)
                        {
                            SendClientMessage(Color.OrangeRed, "You exceed maximum login tries. You have been kicked!");
                            _kickTimer = new Timer(1500, false);
                            _kickTimer.Tick += _kickTimer_Tick;
                        }
                        else if (BCryptHelper.CheckPassword(ev.InputText, player.Password))
                        {
                            SetSpawnInfo(NoTeam, 0, GetPlayerPositionVector3(), player.FacingAngle);
                            Spawn();
                        }
                        else
                        {
                            SendClientMessage(Color.Red, "Wrong password");
                            LoginPlayer();
                            _loginTries++;
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