using GamemodeDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;

namespace BasicGamemode;

public class Startup : IStartup
{
    public void Configure(IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

        var databaseServerVersion = new MariaDbServerVersion("10.5");
        services.AddSingleton(configuration)
            .AddSystemsInAssembly()
            .AddDbContextPool<GamemodeContext>(options => options.UseMySql(configuration.GetConnectionString("Default"), databaseServerVersion));
    }

    public void Configure(IEcsBuilder builder)
    {
        builder.EnableSampEvents()
            .EnablePlayerCommands();
    }
}

