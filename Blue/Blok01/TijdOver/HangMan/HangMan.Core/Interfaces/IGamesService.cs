namespace HangMan.Core.Interfaces; 
public interface IGamesService {
    Task<Game> CreateGameAsync(int playerId);
    Task<IEnumerable<Game>> GetStatsAsync();
    Task<Game> GuessLetterAsync(int gameId, char letter);
}
