using BasicGamemode.Components;
using GamemodeDatabase;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace BasicGamemode.Commands;

public class GeneralCommands : ISystem
{
    [PlayerCommand]
    public void PasswordChange(PlayerAccountComponent playerAccount, IDialogService dialogService, GamemodeContext context)
    {
        var changePasswordDialog = new InputDialog { Caption = "Change your password", Content = "Insert your password", IsPassword = true, Button1 = "Submit", Button2 = "Cancel" };

        void ChangedPasswordDialogHandler(InputDialogResponse r)
        {
            if (r.Response == DialogResponse.LeftButton)
            {
                var player = playerAccount.GetComponent<Player>();
                if (BCrypt.Net.BCrypt.EnhancedVerify(r.InputText, playerAccount.Account.Password))
                {
                    player.SendClientMessage(Color.Aqua, "You must input a different password! The password can't be the same as the old one!");
                }
                else
                {
                    playerAccount.Account.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(r.InputText);
                    context.SaveChanges();
                    player.SendClientMessage(Color.Aqua, "Your password was changed!");
                }
            }
        }

        dialogService.Show(playerAccount, changePasswordDialog, ChangedPasswordDialogHandler);
    }
}
