using HangMan.Core.Interfaces;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("HangMan.Core.UnitTests")]
namespace HangMan.Core;
public class Game {
    private IWordSelector wordSelector;

    public Game() {
        
    }
    public int Id { get; set; }
    public string SecretWord { get; set; }
    public SortedSet<Guess> Guesses { get; set; } = new SortedSet<Guess>();

    

    public List<int> GuessLetter(char letter) {
        List<int> positions = new List<int>();
        bool isRightGuess = false;
        for (int i = 0; i < SecretWord.Length; i++) {
            if (SecretWord[i] == letter) {
                isRightGuess = true;
                positions.Add(i);
            }
        }
        Guesses.Add(new Guess(letter,isRightGuess));
        return positions;
    }

    public IEnumerable<Guess> WrongGuesses => Guesses.Where(g => !g.IsRightGuess);
    public IEnumerable<Guess> RightGuesses => Guesses.Where(g => g.IsRightGuess);

    public string GuessedWord {
        get {
            StringBuilder builder = new();
            for (int i = 0; i < SecretWord.Length; i++) {
                if (Guesses.Any(g => g.Letter == SecretWord[i])) {
                    builder.Append(SecretWord[i]);
                } else {
                    builder.Append('_');
                }
            }
            return builder.ToString();
        }
    }

    public int AvailableNumberOfGuesses => 6 - WrongGuesses.Count();

    public bool GameOver => WordHasBeenGuessed || AvailableNumberOfGuesses == 0;

    public bool WordHasBeenGuessed => SecretWord.Distinct().OrderBy(c=>c).SequenceEqual(RightGuesses.Select(g=>g.Letter));

    public int PlayerId { get; set; }
    
}
