using BasicGamemode;
using SampSharp.Core;
using SampSharp.Entities;

new GameModeBuilder()
    .UseEcs<Startup>()
    .Run();


