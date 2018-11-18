using System.Threading.Tasks;

namespace GamemodeDatabase
{
    internal class Program
    {
        private static async Task Main()
        {
            using (var db = new GamemodeContext())
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();
            }
        }
    }
}