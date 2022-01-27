using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GamemodeDatabase;

public class GamodeDesignTimeDbContextFactory : IDesignTimeDbContextFactory<GamemodeContext>
{
    public GamemodeContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

        var databaseServerVersion = new MariaDbServerVersion("10.5");
        var optionsBuilder = new DbContextOptionsBuilder<GamemodeContext>();
        optionsBuilder.UseMySql(configuration.GetConnectionString("Default"), databaseServerVersion);

        return new GamemodeContext(optionsBuilder.Options);
    }
}