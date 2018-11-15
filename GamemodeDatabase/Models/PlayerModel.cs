using System.ComponentModel.DataAnnotations;

namespace GamemodeDatabase.Models
{
    public class PlayerModel
    {


        public int Id { get; set; }
        public string Password { get; set; }
        [MaxLength(25)]
        public string PlayerName { get; set; }
    }
}