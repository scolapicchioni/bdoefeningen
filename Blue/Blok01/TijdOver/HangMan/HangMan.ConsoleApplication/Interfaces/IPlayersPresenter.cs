using HangMan.Core.Entities;

namespace HangMan.ConsoleApplication.Interfaces;
public interface IPlayersPresenter {
    Task<Player> SelectPlayer();
}
