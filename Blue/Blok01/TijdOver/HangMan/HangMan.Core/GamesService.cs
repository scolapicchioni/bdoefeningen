using HangMan.Core.Entities;
using HangMan.Core.Interfaces;

namespace HangMan.Core {
    public class GamesService : IGamesService {
        private IGamesRepository repository;
        IWordSelector wordSelector;

        public GamesService(IGamesRepository repository, IWordSelector wordSelector) {
            this.repository = repository;
            this.wordSelector = wordSelector;
        }

        public async Task<IEnumerable<Game>> GetStatsAsync() => (await repository.GetGamesAsync())
                        .Where(g => g.GameOver)
                        .OrderByDescending(g => g.Id)
                        .Take(10);

        public async Task<Game> CreateGameAsync(int playerId) {
            Game game = new Game() { PlayerId = playerId };
            game.SecretWord = await wordSelector.SelectWord();
            return await repository.AddGameAsync(game);
        }

        public async Task<Game> GuessLetterAsync(int gameId, char letter) {
            Game game = await repository.GetGameByIdAsync(gameId);
            game.GuessLetter(letter);
            return await repository.UpdateGameAsync(game);
        }
    }
}