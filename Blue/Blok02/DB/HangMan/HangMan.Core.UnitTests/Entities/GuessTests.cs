using HangMan.Core.Entities;
using Xunit;

namespace HangMan.Core.UnitTests.Entities;
public class GuessTests {
    [Fact]
    public void Guess_SetLetterToSmallCase() {
        Guess guess = new Guess('A', false);
        Assert.Equal('a', guess.Letter);
    }
    [Fact]
    public void Guess_Throws_WhenLetterIsNotInAlphabet() {
        Assert.Throws<ArgumentException>(() => new Guess('!', true));
    }
    [Fact]
    public void WrongGuessIsSmallerThanRightGuessWithSameLetter() {
        Guess smallerGuess = new Guess('a', false);
        Guess biggerGuess = new Guess('a', true);

        Assert.True(smallerGuess.CompareTo(biggerGuess) < 0);
    }
    [Fact]
    public void WrongGuessLastLetterIsSmallerThanRightGuessFirstLetter() {
        Guess smallerGuess = new Guess('z', false);
        Guess biggerGuess = new Guess('a', true);

        Assert.True(smallerGuess.CompareTo(biggerGuess) < 0);
    }
    [Fact]
    public void SameLetter_AreEqual_WhenBothRight() {
        Guess guess1 = new Guess('a', true);
        Guess guess2 = new Guess('a', true);

        Assert.True(guess1.CompareTo(guess2) == 0);
    }
    [Fact]
    public void RightGuessIsBiggerThanWrongGuessWithSameLetter() {
        Guess smallerGuess = new Guess('a', false);
        Guess biggerGuess = new Guess('a', true);

        Assert.True(biggerGuess.CompareTo(smallerGuess) > 0);
    }
    [Fact]
    public void RightGuessLastLetterIsBiggerThanWrongGuessFirstLetter() {
        Guess smallerGuess = new Guess('z', false);
        Guess biggerGuess = new Guess('a', true);

        Assert.True(biggerGuess.CompareTo(smallerGuess) > 0);
    }
}
