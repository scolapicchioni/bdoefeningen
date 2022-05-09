using HangMan.ConsoleApplication.Interfaces;

namespace HangMan.ConsoleApplication.Views;
public class GameView : IGameView {
    private string[] puppets = {
@"
-----
|   |
|     
|    
|     
",
@"
-----
|   |
|   O
|     
|     
",
@"
-----
|   |
|   O
|   |  
|     
",
@"
-----
|   |
|   O
|  /| 
|     
",
@"
-----
|   |
|   O
|  /|\
|     
",
@"
-----
|   |
|   O
|  /|\
|  / 
",
@"
-----
|   |
|   O
|  /|\
|  / \
"};

    public GameView() {
    }

    public List<char> WrongGuesses { get => throw new NotImplementedException(); set => value.ForEach(c => Console.WriteLine("Wrong guess: " + c)); }
    public int AvailableNumberOfAttempts { get => throw new NotImplementedException(); set => Console.WriteLine(puppets[6 - value]); }
    public string GuessedWord { get => throw new NotImplementedException(); set => Console.WriteLine(value); }
    public bool GameOver {
        get => throw new NotImplementedException();
        set {
            if (value) {
                Console.WriteLine("Game Over!");
            }
        }
    }
    public bool WordHasBeenGuessed { get => throw new NotImplementedException(); set => Console.WriteLine($"Word guessed: {value}"); }
    public string SecretWord { get => throw new NotImplementedException(); set => Console.WriteLine($"Secret Word: {value}"); }

    public bool AskToPlayAgain() {
        Console.WriteLine("Play Again? (y/n)");
        char input = Console.ReadKey().KeyChar;
        return input == 'y';
    }

    public void Clear() {
        Console.Clear();
    }

    public char GuessLetter() {
        Console.Write("Guess A Letter: ");
        char input = Console.ReadKey().KeyChar;
        Console.WriteLine();
        return input;
    }

    public void Show() {
        Console.Clear();
    }
}
