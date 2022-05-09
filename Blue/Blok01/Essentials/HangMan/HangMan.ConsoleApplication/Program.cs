using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using HangMan.Core;
using HangMan.Infrastructure;
using HangMan.ConsoleApplication;

using IHost host = CreateHostBuilder(args).Build();

IGamePresenter presenter = host.Services.GetRequiredService<IGamePresenter>();

await presenter.Show();

await host.RunAsync();

IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) => {
                services.AddSingleton<IWordSelector, WordFileSelector>();
                services.AddScoped<Game>();
                services.AddScoped<IGameView, GameView>();
                services.AddScoped<IGamePresenter, GamePresenter>();
            });