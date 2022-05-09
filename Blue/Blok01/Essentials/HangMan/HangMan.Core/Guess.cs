namespace HangMan.Core; 
public class Guess : IComparable<Guess> {
    public Guess(char letter, bool rightGuess) {
        Letter = letter;
        IsRightGuess = rightGuess;
    }

    private char letter;
    public char Letter {
        get { return letter; } 
        private set { 
            if(!Char.IsLetter(value)) throw new ArgumentException("Not a letter of the alphabet");
            letter = Char.ToLower(value);
        } 
    }
    public bool IsRightGuess { get; }

    public int CompareTo(Guess? other) => Letter.CompareTo(other.Letter) + (100 * IsRightGuess.CompareTo(other.IsRightGuess));
}
