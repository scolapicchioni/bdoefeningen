using HangMan.Core.Entities;
using HangMan.Core.Interfaces;

namespace HangMan.Core.Services; 
public class GamesService : IGamesService {
    private IGamesRepository repository;
    IWordsService wordService;

    public GamesService(IGamesRepository repository, IWordsService wordService) {
        this.repository = repository;
        this.wordService = wordService;
    }

    public async Task<IEnumerable<Game>> GetStatsAsync() => (await repository.GetGamesAsync())
                    .Where(g => g.Status != GameStatus.InProgress)
                    .OrderByDescending(g => g.Id)
                    .Take(10);

    public async Task<Game> CreateGameAsync(int playerId) {
        Game game = new Game() { PlayerId = playerId };
        game.SecretWord = await wordService.SelectWord();
        return await repository.AddGameAsync(game);
    }

    public async Task<Game> GuessLetterAsync(int gameId, char letter) {
        Game game = await repository.GetGameByIdAsync(gameId);
        game.GuessLetter(letter);
        setStatus(game);
        return await repository.UpdateGameAsync(game);
    }

    private static void setStatus(Game game) {
        bool wordHasBeenGuessed = game.SecretWord.Distinct().OrderBy(c => c).SequenceEqual(game.RightGuesses.Select(g => g.Letter));
        if (wordHasBeenGuessed) {
            game.Status = GameStatus.Won;
        } else if (game.AvailableNumberOfGuesses > 0) {
            game.Status = GameStatus.InProgress;
        } else {
            game.Status = GameStatus.Lost;
        }
    }
}
