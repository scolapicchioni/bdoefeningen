namespace HangMan.ConsoleApplication.Interfaces;
public interface IGameView {
    void Show();
    void Clear();
    List<char> WrongGuesses { get; set; }
    int AvailableNumberOfAttempts { get; set; }
    string GuessedWord { get; set; }
    bool GameOver { get; set; }
    bool WordHasBeenGuessed { get; set; }
    char GuessLetter();
    string SecretWord { get; set; }
    bool AskToPlayAgain();
}
