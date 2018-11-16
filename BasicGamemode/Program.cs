using SampSharp.Core;
using SampSharp.Core.Logging;

namespace BasicGamemode
{
    internal class Program
    {
        private static void Main()
        {
            new GameModeBuilder()
                .Use<GameMode>()
                .Run();
        }
    }
}