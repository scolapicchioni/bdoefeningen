using HangMan.Core;

namespace HangMan.ConsoleApplication.Interfaces;
public interface IGameStatisticsView {
    IEnumerable<Game> Games { get; set; }
    void ShowGamesStatistics();
}
