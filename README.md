# Basic SA-MP GameMode with S#
It is a basic GameMode wrote in C# using the [Sampsharp](https://github.com/ikkentim/SampSharp) framework to give a basic startup for people that want to build their SA-MP gamemodes in C# and don't know where to start

### Getting started
Clone or fork this project into your machine and open up the project solution using Visual Studio 2022

Refer to [SampSharp Documentation](https://sampsharp.net/getting-started) for further server setup

### How to run EF Core migration on this project
* Comment this code inside GamemodeContext.cs
```c#
public GamemodeContext(DbContextOptions<GamemodeContext> options) : base(options)
{
}
```
* Uncomment this code
```c#
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    var databaseServerVersion = new MariaDbServerVersion("10.5");
    optionsBuilder.UseMySql("connectionString", databaseServerVersion);
}
```
* Run EF Core tools commands from Package Manager Console inside Visual Studio with Default Project set to \"GamemodeDatabase\" (recommended)

### Contribute
To fix a bug or enhance an existing module, follow these steps:

* Fork the repo
* Create a new branch (git checkout -b improve-feature)
* Make the appropriate changes in the files
* Add changes to reflect the changes made
* Commit your changes (git commit -am 'Improve feature')
* Push to the branch (git push origin improve-feature)
* Create a Pull Request

### Bug/Feature request
If you found a bug or want a feature open an issue [here](https://github.com/CiprianN23/SAMP-BasicGamemode/issues)

### Built with
* [Visual Studio 2022 Comunity Preview](https://visualstudio.microsoft.com/downloads/)
* [.NET 6.0](https://github.com/dotnet/core)
* [SampSharp 0.9.*](https://github.com/ikkentim/SampSharp)
