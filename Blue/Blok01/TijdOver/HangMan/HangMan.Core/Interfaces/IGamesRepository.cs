using HangMan.Core.Entities;

namespace HangMan.Core.Interfaces {
    public interface IGamesRepository {
        Task<Game> GetGameByIdAsync(int id);
        Task<IQueryable<Game>> GetGamesAsync();
        Task<Game> AddGameAsync(Game game);
        Task<Game> UpdateGameAsync(Game game);
    }
}