using HangMan.Core.Entities;
using HangMan.Core.Interfaces;
using HangMan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HangMan.Infrastructure.Repositories.EF;
public class GamesRepository : IGamesRepository {
    private readonly HangManDbContext context;

    public GamesRepository(HangManDbContext context) {
        this.context = context;
    }
    public async Task<Game> AddGameAsync(Game game) {
        await context.Guesses.AddRangeAsync(game.Guesses);
        await context.Games.AddRangeAsync(game);
        await context.SaveChangesAsync();
        return game;
    }

    public async Task<Game> GetGameByIdAsync(int id) {
        return await context.Games.Include(g => g.Guesses).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IQueryable<Game>> GetGamesAsync() {
        return context.Games.Include(g => g.Guesses);
    }

    public async Task<Game> UpdateGameAsync(Game game) {
        context.Guesses.UpdateRange(game.Guesses);
        context.Games.Update(game);
        await context.SaveChangesAsync();
        return game;
    }
}
