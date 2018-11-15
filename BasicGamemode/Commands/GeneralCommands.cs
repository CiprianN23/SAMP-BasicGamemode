using BasicGamemode.World;
using BCrypt;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace BasicGamemode.Commands
{
    public class GeneralCommands
    {
        [Command("changepassword")]
        private static void Changepassword(BasePlayer sender)
        {
            var player = sender as Player;
            var playerData = player.Account;

            var dialog = new InputDialog("Change your password", "Insert your password", true, "Submit", "Cancel");
            dialog.Show(sender);
            dialog.Response += (senderPlayer, ev) =>
            {
                switch (ev.DialogButton)
                {
                    case DialogButton.Left:
                    {
                        var salt = BCryptHelper.GenerateSalt(10);
                        var hash = BCryptHelper.HashPassword(ev.InputText, salt);
                        if (BCryptHelper.CheckPassword(ev.InputText, playerData.Password))
                        {
                            sender.SendClientMessage(Color.Aqua, "You must input a different password! The password can't be the same as the old one!");
                        }
                        else
                        {
                            playerData.Password = hash;
                            player.UpdatePlayerData(playerData);
                            player.SendClientMessage(Color.Aqua, "Your password was changed!");
                        }
                    }
                        break;
                }
            };
        }
    }
}