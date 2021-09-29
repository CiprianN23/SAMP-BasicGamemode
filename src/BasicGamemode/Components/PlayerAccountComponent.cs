using GamemodeDatabase.Models;
using SampSharp.Entities;

namespace BasicGamemode.Components;

public class PlayerAccountComponent : Component
{
    public PlayerModel Account { get; set; }
    public int LoginTries { get; set; }
}

