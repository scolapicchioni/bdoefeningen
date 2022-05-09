using HangMan.Core.Entities;
using HangMan.Core.Interfaces;

namespace HangMan.Core;
public class PlayersStatisticsService : IPlayersStatisticsService {
    private IPlayersRepository playersRepository;
    private IGamesRepository gamesRepository;

    public PlayersStatisticsService(IPlayersRepository playersRepository, IGamesRepository gamesRepository) {
        this.playersRepository = playersRepository;
        this.gamesRepository = gamesRepository;
    }

    public async Task<IEnumerable<PlayerStats>> GetPlayersWithMostGuessedWordsAsync() {
        IQueryable<Player> players = await playersRepository.GetPlayersAsync();
        IQueryable<Game> games = await gamesRepository.GetGamesAsync();
        var q = from game in games
                join player in players
                on game.PlayerId equals player.Id
                group game by new { player.Id, player.Name }
                into gamesByPlayer
                let guessedWords = gamesByPlayer.Count(g => g.WordHasBeenGuessed)
                let notGuessedWords = gamesByPlayer.Count(g => !g.WordHasBeenGuessed)
                let percent = Math.Round(100 * guessedWords / (double)(guessedWords + notGuessedWords), 1)
                orderby guessedWords descending, gamesByPlayer.Key.Id
                select new PlayerStats() { PlayerId = gamesByPlayer.Key.Id, PlayerName = gamesByPlayer.Key.Name, GuessedWords = guessedWords, NotGuessedWords = notGuessedWords, PercentGuessed = percent };
        return q;
    }

    public async Task<IEnumerable<PlayerStats>> GetPlayersWithBestPercentGuessedAsync() {
        IQueryable<Player> players = await playersRepository.GetPlayersAsync();
        IQueryable<Game> games = await gamesRepository.GetGamesAsync();
        var q = from game in games
                join player in players
                on game.PlayerId equals player.Id
                group game by new { player.Id, player.Name }
                into gamesByPlayer
                let guessedWords = gamesByPlayer.Count(g => g.WordHasBeenGuessed)
                let notGuessedWords = gamesByPlayer.Count(g => !g.WordHasBeenGuessed)
                let percent = Math.Round(100 * guessedWords / (double)(guessedWords + notGuessedWords), 1)
                orderby percent descending, gamesByPlayer.Key.Id
                select new PlayerStats() { PlayerId = gamesByPlayer.Key.Id, PlayerName = gamesByPlayer.Key.Name, GuessedWords = guessedWords, NotGuessedWords = notGuessedWords, PercentGuessed = percent };
        return q;
    }
}
