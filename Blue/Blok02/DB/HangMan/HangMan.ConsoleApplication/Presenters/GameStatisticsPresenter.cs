using HangMan.ConsoleApplication.Interfaces;
using HangMan.Core.Interfaces;

namespace HangMan.ConsoleApplication.Presenters;
public class GameStatisticsPresenter : IGameStatisticsPresenter {
    private readonly IGameStatisticsView view;
    private readonly IGamesService gamesService;

    public GameStatisticsPresenter(IGameStatisticsView view, IGamesService gamesService) {
        this.view = view;
        this.gamesService = gamesService;
    }
    public async Task Show() {
        view.Games = await gamesService.GetStatsAsync();
        view.ShowGamesStatistics();
    }
}
