namespace HangMan.Core.Entities;
public class Guess : IComparable<Guess> {
    public Guess() {

    }
    public Guess(char letter, bool rightGuess) {
        Letter = letter;
        IsRightGuess = rightGuess;
    }

    public int Id { get; set; }
    public int GameId { get; set; }
    public Game Game { get; set; }

    private char letter;
    public char Letter {
        get { return letter; }
        set {
            if (!char.IsLetter(value)) throw new ArgumentException("Not a letter of the alphabet");
            letter = char.ToLower(value);
        }
    }
    public bool IsRightGuess { get; set; }

    public int CompareTo(Guess? other) => Letter.CompareTo(other.Letter) + 100 * IsRightGuess.CompareTo(other.IsRightGuess);
}
