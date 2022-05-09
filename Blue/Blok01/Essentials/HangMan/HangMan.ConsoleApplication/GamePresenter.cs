using HangMan.Core;

namespace HangMan.ConsoleApplication; 
public class GamePresenter : IGamePresenter {
    private readonly Game game;
    private readonly IGameView view;

    public GamePresenter(Game game, IGameView view) {
        this.game = game;
        this.view = view;
    }
    public async Task Show() {
        bool playAgain = true;
        while (playAgain) {
            await game.Reset();
            UpdateView();
            do {
                char letter = view.GuessLetter();
                game.GuessLetter(letter);
                UpdateView();
            } while (!game.GameOver);
            view.SecretWord = game.SecretWord;
            playAgain = view.AskToPlayAgain();
        }
    }
    private void UpdateView() {
        view.Clear();
        view.AvailableNumberOfAttempts = game.AvailableNumberOfGuesses;
        view.GuessedWord = game.GuessedWord;
        view.WrongGuesses = game.WrongGuesses.Select(g => g.Letter).ToList();
        view.GameOver = game.GameOver;
        view.WordHasBeenGuessed = game.WordHasBeenGuessed;
    }
}
