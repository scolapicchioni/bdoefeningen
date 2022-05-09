using HangMan.Core.Entities;

namespace HangMan.Core.Interfaces; 
public interface IPlayersService {
    Task<Player?> AddPlayer(Player playerToAdd);
    Task<Player?> GetPlayerById(int playerId);
    Task<Player?> GetPlayerByName(string playerName);
    Task<IEnumerable<Player>> GetPlayersAsync(Range range);
}
