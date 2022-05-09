using HangMan.ConsoleApplication.Interfaces;
using HangMan.Core.Entities;
using HangMan.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HangMan.ConsoleApplication.Presenters;
public class MainPresenter : IHostedService {
    private readonly IGamesService gamesService;
    private readonly IServiceProvider serviceProvider;
    private readonly IMainView mainView;

    public MainPresenter(IServiceProvider serviceProvider, IMainView mainView) {            
        this.serviceProvider = serviceProvider;
        this.mainView = mainView;
    }

    public async Task StartAsync(CancellationToken cancellationToken) {
        Player player = null;
        int userChoice;
        do {
            mainView.Clear();
            mainView.PlayerSelected(player);
            userChoice = mainView.SelectFeature();
            switch (userChoice) {
                case 1:
                    player = await SelectPlayer();
                    break;
                case 2:
                    await PlayGame(player);
                    break;
                case 3:
                    await GameStatistics();
                    break;
                case 4:
                    await PlayersStatistics();
                    break;
                default:
                    break;
            }
        } while (userChoice != 0);
    }

    private async Task PlayersStatistics() {
        IPlayersStatisticsPresenter playersStatisticsPresenter = serviceProvider.GetRequiredService<IPlayersStatisticsPresenter>();
        await playersStatisticsPresenter.Show();
    }

    private async Task GameStatistics() {
        IGameStatisticsPresenter gameStatisticsPresenter = serviceProvider.GetRequiredService<IGameStatisticsPresenter>();
        await gameStatisticsPresenter.Show();
    }

    private async Task<Player> SelectPlayer() {
        IPlayersPresenter playersPresenter = serviceProvider.GetRequiredService<IPlayersPresenter>();
        return await playersPresenter.SelectPlayer();
    }

    private async Task PlayGame(Player player) {
        if (player == null) {
            mainView.Clear();
            mainView.PlayerSelected(player);
        } else {
            IGamePresenter gamePresenter = serviceProvider.GetRequiredService<IGamePresenter>();
            gamePresenter.PlayerId = player.Id;
            await gamePresenter.Show();
        }
        
    }

    public Task StopAsync(CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}
