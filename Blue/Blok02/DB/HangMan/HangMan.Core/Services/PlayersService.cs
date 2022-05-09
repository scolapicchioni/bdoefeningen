using HangMan.Core.Entities;
using HangMan.Core.Interfaces;

namespace HangMan.Core.Services;
public class PlayersService : IPlayersService {
    private IPlayersRepository repository;

    public PlayersService(IPlayersRepository repository) {
        this.repository = repository;
    }

    public async Task<Player?> AddPlayer(Player playerToAdd) {
        Player? player = await repository.AddPlayerAsync(playerToAdd);
        if (player == null) {
            throw new InvalidOperationException("Player with same name already exists");
        }
        return player;
    }

    public async Task<Player?> GetPlayerByName(string playerName) => await repository.GetPlayerByNameAsync(playerName);

    public async Task<Player?> GetPlayerById(int playerId) => await repository.GetPlayerByIdAsync(playerId);

    public async Task<IEnumerable<Player>> GetPlayersAsync(Range range) {
        if (range.End.IsFromEnd && range.End.Value == 0) {
            return (await repository.GetPlayersAsync()).OrderBy(p => p.Name).Skip(range.Start.Value);
        }
        return (await repository.GetPlayersAsync()).OrderBy(p => p.Name).Skip(range.Start.Value).Take(range.End.Value - range.Start.Value);
    }
}
