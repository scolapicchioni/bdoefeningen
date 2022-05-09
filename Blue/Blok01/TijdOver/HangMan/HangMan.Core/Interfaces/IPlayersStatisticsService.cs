using HangMan.Core.Entities;

namespace HangMan.Core.Interfaces; 
public interface IPlayersStatisticsService {
    Task<IEnumerable<PlayerStats>> GetPlayersWithBestPercentGuessedAsync();
    Task<IEnumerable<PlayerStats>> GetPlayersWithMostGuessedWordsAsync();
}
