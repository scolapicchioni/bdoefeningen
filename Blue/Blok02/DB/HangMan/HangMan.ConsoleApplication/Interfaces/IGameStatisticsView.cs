using HangMan.Core.Entities;

namespace HangMan.ConsoleApplication.Interfaces;
public interface IGameStatisticsView {
    IEnumerable<Game> Games { get; set; }
    void ShowGamesStatistics();
}
