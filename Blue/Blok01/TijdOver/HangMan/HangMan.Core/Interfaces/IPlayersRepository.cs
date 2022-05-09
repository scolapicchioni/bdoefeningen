using HangMan.Core.Entities;

namespace HangMan.Core.Interfaces;
public interface IPlayersRepository {
    Task<Player?> GetPlayerByNameAsync(string playerName);
    Task<Player?> AddPlayerAsync(Player playerToAdd);
    Task<Player?> GetPlayerByIdAsync(int playerId);
    Task<IQueryable<Player>> GetPlayersAsync();
}
