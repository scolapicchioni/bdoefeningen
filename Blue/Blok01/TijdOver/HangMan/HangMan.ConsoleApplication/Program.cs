using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using HangMan.Core;
using HangMan.Infrastructure;
using HangMan.Core.Interfaces;
using HangMan.Infrastructure.Repositories.Memory;
using HangMan.ConsoleApplication.Presenters;
using HangMan.ConsoleApplication.Views;
using HangMan.ConsoleApplication.Interfaces;

using IHost host = CreateHostBuilder(args).Build();

await host.RunAsync();

IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) => {
                services.AddHostedService<MainPresenter>();
                services.AddSingleton<IMainView, MainView>();
                
                services.AddSingleton<IWordSelector, WordFileSelector>();
                services.AddSingleton<IGameView, GameView>();
                services.AddSingleton<IGamePresenter, GamePresenter>();
                services.AddSingleton<IGamesService, GamesService>();
                services.AddSingleton<IGamesRepository, GamesRepository>();

                services.AddSingleton<IGameStatisticsView, GameStatisticsView>();
                services.AddSingleton<IGameStatisticsPresenter, GameStatisticsPresenter>();

                services.AddSingleton<IPlayersRepository, PlayersRepository>();
                services.AddSingleton<IPlayersService, PlayersService>();
                services.AddSingleton<IPlayersView, PlayersView>();
                services.AddSingleton<IPlayersPresenter, PlayersPresenter>();

                services.AddSingleton<IPlayersStatisticsService, PlayersStatisticsService>();
                services.AddSingleton<IPlayersStatisticsView, PlayersStatisticsView>();
                services.AddSingleton<IPlayersStatisticsPresenter, PlayersStatisticsPresenter>();

            });