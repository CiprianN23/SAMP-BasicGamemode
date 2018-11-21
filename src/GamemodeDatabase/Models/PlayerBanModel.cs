using System;
using System.ComponentModel.DataAnnotations;

namespace GamemodeDatabase.Models
{
    public class PlayerBanModel
    {
        public int Id { get; set; }

        [MaxLength(25)] public string AdminName { get; set; }
        [MaxLength(25)] public string BannedPlayerName { get; set; }
        [MaxLength(129)] public string Reason { get; set; }

        public DateTime BanIssueTime { get; set; }
        public DateTime TotalBanTime { get; set; }
    }
}