using System;
using System.ComponentModel.DataAnnotations;

namespace GamemodeDatabase.Models
{
    public class PlayerModel
    {
        public int Id { get; set; }

        [MaxLength(25)] public string PlayerName { get; set; }

        [MaxLength(61)] public string Password { get; set; }

        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public float FacingAngle { get; set; }

        public int Money { get; set; }
        public int Score { get; set; }

        public byte AdminLevel { get; set; }

        public DateTime JoinDate { get; set; }
        public DateTime LastActive { get; set; }
        
    }
}