using HangMan.Core;
using HangMan.Core.Entities;

namespace HangMan.ConsoleApplication.Interfaces; 
public interface IPlayersStatisticsView {
    IEnumerable<PlayerStats> PlayersStats { get; set; }
    string Title { get; set; }
    void Show();
    int Choice();
}
