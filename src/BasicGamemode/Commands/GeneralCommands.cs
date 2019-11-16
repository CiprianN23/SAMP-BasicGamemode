using BasicGamemode.World;
using BCrypt;
using GamemodeDatabase;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace BasicGamemode.Commands
{
    /// <summary>
    /// A class containing general use commands for the server
    /// </summary>
    public class GeneralCommands
    {
        /// <summary>
        /// Comamnd used by Players to change their account password
        /// </summary>
        /// <param name="sender"></param>
        [Command("changepassword")]
        public static void OnPasswordChangeCommand(BasePlayer sender)
        {
            var player = sender as Player;

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
                            if (BCryptHelper.CheckPassword(ev.InputText, player.FetchAccountData().Password))
                            {
                                sender.SendClientMessage(Color.Aqua, "You must input a different password! The password can't be the same as the old one!");
                            }
                            else
                            {
                                using (var db = new GamemodeContext())
                                {
                                    player.FetchAccountData(db).Password = hash;
                                    db.SaveChanges();
                                }

                                player.SendClientMessage(Color.Aqua, "Your password was changed!");
                            }
                        }
                        break;
                }
            };
        }
    }
}