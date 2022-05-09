using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using HangMan.Infrastructure;
using HangMan.Core.Interfaces;
using HangMan.Infrastructure.Repositories.EF;
using HangMan.ConsoleApplication.Presenters;
using HangMan.ConsoleApplication.Views;
using HangMan.ConsoleApplication.Interfaces;
using HangMan.Core.Services;

using IHost host = CreateHostBuilder(args).Build();

await host.RunAsync();

IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) => {
                services.AddHostedService<MainPresenter>();
                services.AddSingleton<IMainView, MainView>();
                
                services.AddScoped<IWordsRepository, WordsRepository>();
                services.AddScoped<IWordsService, WordsService>();

                services.AddScoped<IGameView, GameView>();
                services.AddScoped<IGamePresenter, GamePresenter>();
                services.AddScoped<IGamesService, GamesService>();
                services.AddScoped<IGamesRepository, GamesRepository>();

                services.AddScoped<IGameStatisticsView, GameStatisticsView>();
                services.AddScoped<IGameStatisticsPresenter, GameStatisticsPresenter>();

                services.AddScoped<IPlayersRepository, PlayersRepository>();
                services.AddScoped<IPlayersService, PlayersService>();
                services.AddScoped<IPlayersView, PlayersView>();
                services.AddScoped<IPlayersPresenter, PlayersPresenter>();

                services.AddScoped<IPlayersStatisticsService, PlayersStatisticsService>();
                services.AddScoped<IPlayersStatisticsView, PlayersStatisticsView>();
                services.AddScoped<IPlayersStatisticsPresenter, PlayersStatisticsPresenter>();

                services.AddHangManDbContext(hostContext.Configuration.GetSection("ConnectionStrings:HangManContext").Value);
            });