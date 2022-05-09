using HangMan.ConsoleApplication.Interfaces;
using HangMan.Core.Interfaces;

namespace HangMan.ConsoleApplication.Presenters;
public class PlayersStatisticsPresenter : IPlayersStatisticsPresenter {
    private readonly IPlayersStatisticsView view;
    private readonly IPlayersStatisticsService service;

    public PlayersStatisticsPresenter(IPlayersStatisticsView view, IPlayersStatisticsService service) {
        this.view = view;
        this.service = service;
    }
    public async Task Show() {
        int choice = view.Choice();
        while (choice != 0) {
            view.Show();
            switch (choice) {
                case 1:
                    view.Title = "Players With Most Guessed Words Async";
                    view.PlayersStats = await service.GetPlayersWithMostGuessedWordsAsync();
                    break;
                case 2:
                    view.Title = "Players With Best Percent Guessed";
                    view.PlayersStats = await service.GetPlayersWithBestPercentGuessedAsync();
                    break;
            }
            choice = view.Choice();
        }
    }
}
