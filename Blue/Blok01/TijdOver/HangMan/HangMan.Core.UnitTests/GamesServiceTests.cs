using Xunit;
using Moq;
using HangMan.Core.Entities;
using HangMan.Core.Interfaces;

namespace HangMan.Core.UnitTests;
public class GamesServiceTests {
    Mock<IWordSelector> mockWordSelector;
    private List<Game> games;
    Mock<IGamesRepository> mockRepo;
    GamesService sut;
    public GamesServiceTests() {
        mockWordSelector = new Mock<IWordSelector>();

        games = new List<Game>() {
            new Builders.GameBuilder().WithGameId(1).WithPlayerId(1).WithSecretWord("abcd").WithRightGuesses(4).WithWrongGuesses(3).Build(),
            new Builders.GameBuilder().WithGameId(2).WithPlayerId(1).WithSecretWord("efgh").WithRightGuesses(3).WithWrongGuesses(6).Build(),
            new Builders.GameBuilder().WithGameId(3).WithPlayerId(2).WithSecretWord("lmno").WithRightGuesses(2).WithWrongGuesses(3).Build(),
            new Builders.GameBuilder().WithGameId(4).WithPlayerId(2).WithSecretWord("pqrs").WithRightGuesses(3).WithWrongGuesses(6).Build(),
            new Builders.GameBuilder().WithGameId(5).WithPlayerId(2).WithSecretWord("tuwx").WithRightGuesses(4).WithWrongGuesses(3).Build(),
            new Builders.GameBuilder().WithGameId(6).WithPlayerId(2).WithSecretWord("xyzk").WithRightGuesses(4).WithWrongGuesses(1).Build(),
            new Builders.GameBuilder().WithGameId(7).WithPlayerId(2).WithSecretWord("jklm").WithRightGuesses(2).WithWrongGuesses(2).Build(),
            new Builders.GameBuilder().WithGameId(8).WithPlayerId(3).WithSecretWord("qwerty").WithRightGuesses(6).WithWrongGuesses(3).Build(),
            new Builders.GameBuilder().WithGameId(9).WithPlayerId(3).WithSecretWord("poiuyt").WithRightGuesses(4).WithWrongGuesses(6).Build(),
            new Builders.GameBuilder().WithGameId(10).WithPlayerId(3).WithSecretWord("lkjhgf").WithRightGuesses(6).WithWrongGuesses(3).Build(),
            new Builders.GameBuilder().WithGameId(11).WithPlayerId(4).WithSecretWord("asdfgh").WithRightGuesses(6).WithWrongGuesses(1).Build(),
            new Builders.GameBuilder().WithGameId(12).WithPlayerId(4).WithSecretWord("zxcvbnm").WithRightGuesses(6).WithWrongGuesses(6).Build(),
            new Builders.GameBuilder().WithGameId(13).WithPlayerId(5).WithSecretWord("asdfghjkl").WithRightGuesses(0).WithWrongGuesses(6).Build(),
            new Builders.GameBuilder().WithGameId(14).WithPlayerId(5).WithSecretWord("qwertyuiop").WithRightGuesses(1).WithWrongGuesses(3).Build(),
            new Builders.GameBuilder().WithGameId(15).WithPlayerId(6).WithSecretWord("dgfhs").WithRightGuesses(5).WithWrongGuesses(3).Build()
        };
        mockRepo = new Mock<IGamesRepository>();
        sut = new GamesService(mockRepo.Object, mockWordSelector.Object);
    }
    
    [Fact]
    public async Task GetGamesStatistics_ReturnsLast10GamesStats() { 
        mockRepo.Setup(r=>r.GetGamesAsync()).ReturnsAsync(games.AsQueryable());
        
        IEnumerable<Game> stats = await sut.GetStatsAsync();
        Assert.Collection(stats, 
            g=>Assert.Equal(15, g.Id),
            g => Assert.Equal(13, g.Id),
            g => Assert.Equal(12, g.Id),
            g => Assert.Equal(11, g.Id),
            g => Assert.Equal(10, g.Id),
            g => Assert.Equal(9, g.Id),
            g => Assert.Equal(8, g.Id),
            g => Assert.Equal(6, g.Id),
            g => Assert.Equal(5, g.Id),
            g => Assert.Equal(4, g.Id));
    }
    [Fact]
    public async Task NewGame_InvokesRepoAddGame() {
        string expectedSecretWord = "secretword";
        Game expected = new() { Id = 0, SecretWord = expectedSecretWord, PlayerId = 6 };
        mockWordSelector.Setup(ws => ws.SelectWord()).ReturnsAsync(expectedSecretWord);
        mockRepo.Setup(r => r.AddGameAsync(It.IsAny<Game>())).ReturnsAsync(expected);

        Game actual = await sut.CreateGameAsync(6);

        mockRepo.Verify(r => r.AddGameAsync(It.IsAny<Game>()));
        Assert.Equal(6, actual.PlayerId);
        Assert.Equal(expectedSecretWord, actual.SecretWord);
    }

    [Fact]
    public async Task UpdateGameStat_InvokesRepoUpdateGame() {
        Game game = new() { Id = 15, SecretWord = "secretword", PlayerId = 6 };
        mockRepo.Setup(r => r.GetGameByIdAsync(15)).ReturnsAsync(game);
        mockRepo.Setup(r => r.UpdateGameAsync(It.IsAny<Game>())).Verifiable();

        Game actual = await sut.GuessLetterAsync(15,'c');

        mockRepo.Verify(r => r.UpdateGameAsync(game));
    }
    //[Fact]
    //public async Task ResetGame_ShouldInvokeResetWord() {
    //    string expectedWord = "changedword";
    //    wordSelectorMock.Setup(ws => ws.SelectWord()).ReturnsAsync(expectedWord);
    //    Game game = new Game(wordSelectorMock.Object);
    //    game.SecretWord = "firstword";

    //    await game.Reset();

    //    Assert.Equal(expectedWord, game.SecretWord);
    //}
    //[Fact]
    //public async Task ResetGame_ShouldResetRightGuesses() {
    //    Game game = new Game(wordSelectorMock.Object);
    //    game.Guesses = new SortedSet<Guess>() { new Guess('a', true) };

    //    await game.Reset();

    //    Assert.Empty(game.Guesses);
    //}
}
