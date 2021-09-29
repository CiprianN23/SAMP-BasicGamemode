using BasicGamemode.Components;
using GamemodeDatabase;
using GamemodeDatabase.Models;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using System.Linq;
using System.Threading.Tasks;

namespace BasicGamemode.Systems;

public class PlayerAccountSystem : ISystem
{
    [Event]
    public void OnPlayerConnect(Player player, GamemodeContext context, IDialogService dialogService)
    {
        player.SendClientMessage(Color.GreenYellow, $"Welcome to the server {player.Name}! Have fun!");
        player.SetWorldBounds(2500.0f, 1850.0f, 631.2963f, -454.9898f);
        player.ToggleSpectating(true);

        var componentAccount = player.AddComponent<PlayerAccountComponent>();

        componentAccount.Account = context.Players.Where(p => p.Name == player.Name).FirstOrDefault();
        if (componentAccount.Account is null)
        {
            var registerDialog = new InputDialog() { IsPassword = true, Caption = "Register", Content = "Input your password below to register a new account.", Button1 = "Register", Button2 = "Exit" };

            void RegisterDialogHandler(InputDialogResponse r)
            {
                if (r.Response == DialogResponse.LeftButton)
                {
                    var hash = BCrypt.Net.BCrypt.EnhancedHashPassword(r.InputText);
                    componentAccount.Account = new PlayerModel
                    {
                        Name = player.Name,
                        Password = hash,
                        JoinDate = System.DateTime.Now
                    };

                    context.Players.Add(componentAccount.Account);
                    context.SaveChanges();

                    player.ToggleSpectating(false);
                    player.SetSpawnInfo(255, 0, new Vector3(1685.8075, -2239.2583, 13.5469), 179.4454f);
                    player.Spawn();
                }
                else if (r.Response == DialogResponse.RightButtonOrCancel)
                {
                    player.Kick();
                }
            }

            dialogService.Show(player, registerDialog, RegisterDialogHandler);
        }
        else
        {
            var loginDialog = new InputDialog()
            {
                IsPassword = true,
                Caption = "Login",
                Content = $"Input your password below. Tries left: {componentAccount.LoginTries}/{Config.MaximumLoginTries}",
                Button1 = "Login",
                Button2 = "Exit"
            };

            void LoginDialogHandler(InputDialogResponse r)
            {
                if (r.Response == DialogResponse.LeftButton)
                {
                    if (componentAccount.LoginTries >= Config.MaximumLoginTries)
                    {
                        player.SendClientMessage(Color.OrangeRed, "You exceed maximum login tries. You have been kicked!");
                        Task.Delay(1000);
                        player.Kick();
                    }
                    else if (BCrypt.Net.BCrypt.EnhancedVerify(r.InputText, componentAccount.Account.Password))
                    {
                        player.ToggleSpectating(false);
                        player.SetSpawnInfo(255, 0, new Vector3(componentAccount.Account.PositionX, componentAccount.Account.PositionY, componentAccount.Account.PositionZ), componentAccount.Account.FacingAngle);
                        player.Spawn();
                    }
                    else
                    {
                        componentAccount.LoginTries++;
                        player.SendClientMessage(Color.Red, "Wrong password");
                        loginDialog.Content = $"Input your password below. Tries left: {componentAccount.LoginTries}/{Config.MaximumLoginTries}";
                        dialogService.Show(player, loginDialog, LoginDialogHandler);
                    }
                }
                else if (r.Response == DialogResponse.RightButtonOrCancel)
                {
                    player.Kick();
                }
            }

            dialogService.Show(player, loginDialog, LoginDialogHandler);
        }
    }

    [Event]
    public void OnPlayerDisconnect(PlayerAccountComponent playerAccount, DisconnectReason reason, GamemodeContext context)
    {
        playerAccount.Account.LastActive = System.DateTime.Now;
        context.SaveChanges();
    }
}

