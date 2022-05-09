using HangMan.Core.Entities;

namespace HangMan.ConsoleApplication.Interfaces;
public interface IPlayersView {
    string Name { get; }
    Player? Player { get; set; }
    Exception Error { get; set; }

    int Choice();
    void ListPlayers(IEnumerable<Player> enumerable);
}
