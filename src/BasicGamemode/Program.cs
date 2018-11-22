using SampSharp.Core;

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