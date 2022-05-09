using HangMan.ConsoleApplication.Interfaces;
using HangMan.Core.Entities;
using HangMan.Core.Interfaces;

namespace HangMan.ConsoleApplication.Presenters;
public class GamePresenter : IGamePresenter {
    private Game game;
    private readonly IGamesService gameService;
    private readonly IGameView view;

    public int PlayerId { get; set; } = 1;

    public GamePresenter(IGamesService gameService, IGameView view) {
        this.gameService = gameService;
        this.view = view;
    }
    public async Task Show() {
        bool playAgain = true;
        while (playAgain) {
            game = await gameService.CreateGameAsync(PlayerId);
            UpdateView();
            do {
                char letter = view.GuessLetter();
                await gameService.GuessLetterAsync(game.Id, letter);
                UpdateView();
            } while (game.Status == GameStatus.InProgress);
            view.SecretWord = game.SecretWord;
            playAgain = view.AskToPlayAgain();
        }
    }
    private void UpdateView() {
        view.Clear();
        view.AvailableNumberOfAttempts = game.AvailableNumberOfGuesses;
        view.GuessedWord = game.GuessedWord;
        view.WrongGuesses = game.WrongGuesses.Select(g => g.Letter).ToList();
        view.Status = game.Status;
    }
}
