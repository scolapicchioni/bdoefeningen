using Moq;
using Xunit;

namespace HangMan.Core.UnitTests; 
public class GameTests {
    Mock<IWordSelector> wordSelectorMock;
    public GameTests() {
        wordSelectorMock = new Mock<IWordSelector>();
    }
    [Fact]
    public async Task ResetSecretWord_ResetsSecretWord() {
        string expectedWord = "abacus";
        wordSelectorMock.Setup(ws => ws.SelectWord()).ReturnsAsync(expectedWord);
        Game game = new Game(wordSelectorMock.Object);

        await game.ResetSecretWord();

        Assert.Equal(expectedWord, game.SecretWord);
    }

    [Fact]
    public void GuessLetter_ReturnsListWithPositions_WhenLetterIsPresent() {
        string word = "alpacas";
        List<int> expectedPositions = new List<int>() {0,3,5};
        Game game = new Game(wordSelectorMock.Object);
        game.SecretWord = word;

        List<int> positions = game.GuessLetter('a').ToList();

        Assert.Equal(3, positions.Count);
        Assert.True(positions.SequenceEqual(expectedPositions));
    }
    [Fact]
    public void GuessLetter_ReturnsEmptyList_WhenLetterIsNotPresent() {
        string word = "alpacas";
        Game game = new Game(wordSelectorMock.Object);
        game.SecretWord = word;

        List<int> positions = game.GuessLetter('b').ToList();

        Assert.Empty(positions);
    }
    [Fact]
    public void GuessLetter_AddsLetterToWrongGuesses_WhenLetterIsNotPresent() {
        string word = "alpacas";
        Guess expectedGuess = new Guess('b', false);
        Game game = new Game(wordSelectorMock.Object);
        game.SecretWord = word;

        game.GuessLetter('b').ToList();

        Assert.Contains(expectedGuess, game.WrongGuesses);
    }
    [Fact]
    public void GuessLetter_AddsLetterToRightGuesses_WhenLetterIsPresent() {
        string word = "alpacas";
        Guess expectedGuess = new Guess('a', true);
        Game game = new Game(wordSelectorMock.Object);
        game.SecretWord = word;

        game.GuessLetter('a').ToList();

        Assert.Contains(expectedGuess, game.RightGuesses);
    }
    [Fact]
    public void GuessLetter_Throws_WhenLetterHasAlreadyBeenTried() {
        string word = "alpacas";
        Game game = new Game(wordSelectorMock.Object);
        game.SecretWord = word;

        List<int> positions = game.GuessLetter('b').ToList();

        Assert.Empty(positions);
    }
    [Fact]
    public void GuessedWord_Returns_SecretWord_WithUnderscoresForNotGuessedLetters() {
        string word = "alpacas";
        Game game = new Game(wordSelectorMock.Object);
        game.SecretWord = word;
        game.Guesses = new SortedSet<Guess>() { new Guess('a',true), new Guess('b',false), new Guess('p', true) };
        Assert.Equal("a_pa_a_", game.GuessedWord);
    }
    [Fact]
    public void AvailableNumberOfGuesses_Return_6MinusWrongGuesses() {
        string word = "alpacas";
        Game game = new Game(wordSelectorMock.Object);
        game.SecretWord = word;
        game.Guesses = new SortedSet<Guess>() { new Guess('a', true), new Guess('b', false), new Guess('p', true) };
        Assert.Equal(5, game.AvailableNumberOfGuesses);
    }
    [Fact]
    public async Task ResetGame_ShouldInvokeResetWord() {
        string expectedWord = "changedword";
        wordSelectorMock.Setup(ws => ws.SelectWord()).ReturnsAsync(expectedWord);
        Game game = new Game(wordSelectorMock.Object);
        game.SecretWord = "firstword";

        await game.Reset();

        Assert.Equal(expectedWord, game.SecretWord);
    }
    [Fact]
    public async Task ResetGame_ShouldResetRightGuesses() {
        Game game = new Game(wordSelectorMock.Object);
        game.Guesses = new SortedSet<Guess>() { new Guess('a', true) };
        
        await game.Reset();

        Assert.Empty(game.Guesses);
    }
    [Fact]
    public void GameOver_When_NumberOfAvailableAttemptsIs0() {
        Game game = new Game(wordSelectorMock.Object);
        game.SecretWord = "abc";
        game.Guesses = new SortedSet<Guess>() { new Guess('d', false), new Guess('e',false), new Guess('f', false) , new Guess('g', false) , new Guess('h', false) , new Guess('i', false) };
        Assert.True(game.GameOver);
    }
    [Fact]
    public void GameNotOver_When_NumberOfAvailableAttemptsIsGreaterThan0AndWordHasNotBeenGuessed() {
        Game game = new Game(wordSelectorMock.Object);
        game.SecretWord = "abc";
        game.Guesses = new SortedSet<Guess>() { new Guess('d', false), new Guess('e', false), new Guess('f', false) };
        Assert.False(game.GameOver);
    }
    [Fact]
    public void GameOver_When_NumberOfAvailableAttemptsIsGreaterThan0AndWordHasBeenGuessed() {
        Game game = new Game(wordSelectorMock.Object);
        game.SecretWord = "abc";
        game.Guesses = new SortedSet<Guess>() { new Guess('d', false), new Guess('e', false), new Guess('f', false), new Guess('a', true), new Guess('b', true), new Guess('c', true) };
        Assert.True(game.GameOver);
    }
    [Fact]
    public void WordHasBeenGuessed_True_WhenWordComplete() {
        Game game = new Game(wordSelectorMock.Object);
        game.SecretWord = "abc";
        game.Guesses = new SortedSet<Guess>() { new Guess('d', false), new Guess('e', false), new Guess('f', false), new Guess('a', true), new Guess('b', true), new Guess('c', true) };
        Assert.True(game.WordHasBeenGuessed);
    }
}
