using System;
using Microsoft.Extensions.Configuration;

namespace GamemodeDatabase
{
    class Program
    {
        static void Main()
        {
            using (var context = new GamemodeContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }
}
