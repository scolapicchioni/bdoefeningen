using HangMan.Core.Entities;
using HangMan.Core.Interfaces;
using HangMan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HangMan.Infrastructure.Repositories.EF;
public class PlayersRepository : IPlayersRepository {
    private readonly HangManDbContext context;

    public PlayersRepository(HangManDbContext context) {
        this.context = context;
    }
    public async Task<Player?> AddPlayerAsync(Player playerToAdd) {
        context.Players.Add(playerToAdd);
        await context.SaveChangesAsync();
        return playerToAdd;
    }

    public async Task<Player?> GetPlayerByIdAsync(int playerId) {
        return await context.Players.FirstOrDefaultAsync(p=>p.Id == playerId);
    }

    public async Task<Player?> GetPlayerByNameAsync(string playerName) {
        return await context.Players.FirstOrDefaultAsync(p => p.Name == playerName);
    }

    public async Task<IQueryable<Player>> GetPlayersAsync() {
        return context.Players.AsQueryable();
    }
}
