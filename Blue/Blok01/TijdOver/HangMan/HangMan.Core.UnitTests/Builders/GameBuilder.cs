namespace HangMan.Core.UnitTests.Builders;
internal class GameBuilder {
    private string secretWord;
    private int playerId;
    private SortedSet<Guess> guesses ;
    private int gameId;
    public Game Build() {
        Game game = new Game() {
            SecretWord = secretWord,
            PlayerId = playerId,
            Id = gameId,
            Guesses = guesses
        };
        return game;
    }
    public GameBuilder() { 
        secretWord = string.Empty;
        playerId = 0;
        guesses = new SortedSet<Guess>();
        gameId = 0;
    }
    public GameBuilder WithGameId(int gameId) {
        this.gameId = gameId;
        return this;
    }
    public GameBuilder WithPlayerId(int playerId) { 
        this.playerId = playerId;
        return this;
    }
    public GameBuilder WithSecretWord(string secretWord) {
        this.secretWord = secretWord;
        return this;
    }
    public GameBuilder WithRightGuesses(int howMany) {
        for (int i = 0; i < howMany; i++) {
            guesses.Add(new Guess(this.secretWord[i], true));
        }
        return this;
    }
    public GameBuilder WithWrongGuesses(int howMany) {
        string alphabet = "abcdefghijklmnopqrstuvwxyz";
        IEnumerable<char> wrongletters = alphabet.Except(secretWord);
        for (int i = 0; i < howMany; i++) {
            guesses.Add(new Guess(wrongletters.ElementAt(i), false));
        }
        return this;
    }
}
