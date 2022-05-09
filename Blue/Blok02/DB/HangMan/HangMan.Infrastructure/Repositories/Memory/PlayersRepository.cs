using HangMan.Core.Entities;
using HangMan.Core.Interfaces;

namespace HangMan.Infrastructure.Repositories.Memory;
public class PlayersRepository : IPlayersRepository {
    private List<Player> players;
    public PlayersRepository() {
        players = new List<Player>() { 
            new() { Id = 1, Name = "Mario"},
            new() { Id = 2, Name = "Luigi"},
            new() { Id = 3, Name = "Peach"},
            new() { Id = 4, Name = "Daisy"}
        };
    }
    public async Task<Player?> AddPlayerAsync(Player playerToAdd) {
        if ((await GetPlayerByNameAsync(playerToAdd.Name)) == null){
            playerToAdd.Id = players.Any() ? players.Max(p => p.Id) + 1 : 1;
            players.Add(playerToAdd);
            return playerToAdd;
        }else {
            return null;
        }
    }

    public Task<Player?> GetPlayerByIdAsync(int playerId) {
        return Task.FromResult(players.FirstOrDefault(p => p.Id == playerId));
    }

    public Task<Player?> GetPlayerByNameAsync(string playerName) {
        return Task.FromResult(players.FirstOrDefault(p => p.Name.ToLower() == playerName.ToLower()));
    }

    public Task<IQueryable<Player>> GetPlayersAsync() {
        return Task.FromResult(players.AsQueryable());
    }
}
