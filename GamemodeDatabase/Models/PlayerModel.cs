using System.ComponentModel.DataAnnotations;

namespace GamemodeDatabase.Models
{
    public class PlayerModel
    {


        public int Id { get; set; }
        [MaxLength(61)]
        public string Password { get; set; }
        [MaxLength(25)]
        public string PlayerName { get; set; }

    }
}