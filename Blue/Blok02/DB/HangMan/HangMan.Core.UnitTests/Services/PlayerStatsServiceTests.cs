using Xunit;
using Moq;
using HangMan.Core.Interfaces;
using HangMan.Core.Entities;
using HangMan.Core.Services;

namespace HangMan.Core.UnitTests.Services;
public class PlayerStatsServiceTests {
    Mock<IPlayersRepository> mockPlayersRepo;
    Mock<IGamesRepository> mockGamesRepo;
    PlayersStatisticsService sut;
    public PlayerStatsServiceTests() {
        mockPlayersRepo = new();
        mockGamesRepo = new();
        sut = new(mockPlayersRepo.Object, mockGamesRepo.Object);

        IQueryable<Player> players = new List<Player>() {
            new Player(){ Id = 1, Name = "player1" },
            new Player(){ Id = 2, Name = "player2" },
            new Player(){ Id = 3, Name = "player3" },
            new Player(){ Id = 4, Name = "player4" },
            new Player(){ Id = 5, Name = "player5" },
            new Player(){ Id = 6, Name = "player6" }
        }.AsQueryable();

        IQueryable<Game> games = new List<Game>() {
            new Builders.GameBuilder().WithGameId(1).WithPlayerId(1).WithSecretWord("abcd").WithRightGuesses(4).WithWrongGuesses(3).WithGameStatus(GameStatus.Won).Build(),
            new Builders.GameBuilder().WithGameId(2).WithPlayerId(1).WithSecretWord("efgh").WithRightGuesses(4).WithWrongGuesses(2).WithGameStatus(GameStatus.Won).Build(),
            new Builders.GameBuilder().WithGameId(3).WithPlayerId(1).WithSecretWord("abcd").WithRightGuesses(4).WithWrongGuesses(1).WithGameStatus(GameStatus.Won).Build(),
            new Builders.GameBuilder().WithGameId(4).WithPlayerId(1).WithSecretWord("efgh").WithRightGuesses(4).WithWrongGuesses(0).WithGameStatus(GameStatus.Won).Build(),
            new Builders.GameBuilder().WithGameId(5).WithPlayerId(1).WithSecretWord("abcd").WithRightGuesses(4).WithWrongGuesses(3).WithGameStatus(GameStatus.Won).Build(),
            new Builders.GameBuilder().WithGameId(6).WithPlayerId(1).WithSecretWord("efgh").WithRightGuesses(4).WithWrongGuesses(2).WithGameStatus(GameStatus.Won).Build(),
            new Builders.GameBuilder().WithGameId(7).WithPlayerId(1).WithSecretWord("abcd").WithRightGuesses(4).WithWrongGuesses(1).WithGameStatus(GameStatus.Won).Build(),
            new Builders.GameBuilder().WithGameId(8).WithPlayerId(1).WithSecretWord("efgh").WithRightGuesses(4).WithWrongGuesses(0).WithGameStatus(GameStatus.Won).Build(),
            new Builders.GameBuilder().WithGameId(9).WithPlayerId(1).WithSecretWord("abcd").WithRightGuesses(4).WithWrongGuesses(1).WithGameStatus(GameStatus.Won).Build(),
            new Builders.GameBuilder().WithGameId(10).WithPlayerId(1).WithSecretWord("efgh").WithRightGuesses(4).WithWrongGuesses(0).WithGameStatus(GameStatus.Won).Build(),

            new Builders.GameBuilder().WithGameId(11).WithPlayerId(1).WithSecretWord("efgh").WithRightGuesses(0).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(12).WithPlayerId(1).WithSecretWord("abcd").WithRightGuesses(1).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(13).WithPlayerId(1).WithSecretWord("efgh").WithRightGuesses(2).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(14).WithPlayerId(1).WithSecretWord("abcd").WithRightGuesses(3).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(15).WithPlayerId(1).WithSecretWord("efgh").WithRightGuesses(0).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),

            new Builders.GameBuilder().WithGameId(16).WithPlayerId(2).WithSecretWord("abcd").WithRightGuesses(4).WithWrongGuesses(3).WithGameStatus(GameStatus.Won).Build(),
            new Builders.GameBuilder().WithGameId(17).WithPlayerId(2).WithSecretWord("efgh").WithRightGuesses(4).WithWrongGuesses(2).WithGameStatus(GameStatus.Won).Build(),
            new Builders.GameBuilder().WithGameId(18).WithPlayerId(2).WithSecretWord("abcd").WithRightGuesses(4).WithWrongGuesses(1).WithGameStatus(GameStatus.Won).Build(),
            new Builders.GameBuilder().WithGameId(19).WithPlayerId(2).WithSecretWord("efgh").WithRightGuesses(4).WithWrongGuesses(0).WithGameStatus(GameStatus.Won).Build(),
            new Builders.GameBuilder().WithGameId(20).WithPlayerId(2).WithSecretWord("abcd").WithRightGuesses(4).WithWrongGuesses(3).WithGameStatus(GameStatus.Won).Build(),

            new Builders.GameBuilder().WithGameId(21).WithPlayerId(2).WithSecretWord("efgh").WithRightGuesses(0).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(22).WithPlayerId(2).WithSecretWord("abcd").WithRightGuesses(1).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(23).WithPlayerId(2).WithSecretWord("efgh").WithRightGuesses(2).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),

            new Builders.GameBuilder().WithGameId(24).WithPlayerId(3).WithSecretWord("abcd").WithRightGuesses(4).WithWrongGuesses(3).WithGameStatus(GameStatus.Won).Build(),
            new Builders.GameBuilder().WithGameId(25).WithPlayerId(3).WithSecretWord("efgh").WithRightGuesses(4).WithWrongGuesses(2).WithGameStatus(GameStatus.Won).Build(),
            new Builders.GameBuilder().WithGameId(26).WithPlayerId(3).WithSecretWord("abcd").WithRightGuesses(4).WithWrongGuesses(1).WithGameStatus(GameStatus.Won).Build(),
            new Builders.GameBuilder().WithGameId(27).WithPlayerId(3).WithSecretWord("efgh").WithRightGuesses(4).WithWrongGuesses(0).WithGameStatus(GameStatus.Won).Build(),
            new Builders.GameBuilder().WithGameId(28).WithPlayerId(3).WithSecretWord("abcd").WithRightGuesses(4).WithWrongGuesses(3).WithGameStatus(GameStatus.Won).Build(),

            new Builders.GameBuilder().WithGameId(29).WithPlayerId(3).WithSecretWord("efgh").WithRightGuesses(0).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(30).WithPlayerId(3).WithSecretWord("abcd").WithRightGuesses(1).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(31).WithPlayerId(3).WithSecretWord("efgh").WithRightGuesses(2).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(32).WithPlayerId(3).WithSecretWord("efgh").WithRightGuesses(0).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(33).WithPlayerId(3).WithSecretWord("abcd").WithRightGuesses(1).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(34).WithPlayerId(3).WithSecretWord("efgh").WithRightGuesses(2).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(35).WithPlayerId(3).WithSecretWord("efgh").WithRightGuesses(0).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(36).WithPlayerId(3).WithSecretWord("abcd").WithRightGuesses(1).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(37).WithPlayerId(3).WithSecretWord("efgh").WithRightGuesses(2).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(38).WithPlayerId(3).WithSecretWord("efgh").WithRightGuesses(0).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),

            new Builders.GameBuilder().WithGameId(39).WithPlayerId(4).WithSecretWord("abcd").WithRightGuesses(4).WithWrongGuesses(3).WithGameStatus(GameStatus.Won).Build(),

            new Builders.GameBuilder().WithGameId(40).WithPlayerId(4).WithSecretWord("abcd").WithRightGuesses(0).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(55).WithPlayerId(4).WithSecretWord("efgh").WithRightGuesses(0).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),

            new Builders.GameBuilder().WithGameId(41).WithPlayerId(5).WithSecretWord("efgh").WithRightGuesses(4).WithWrongGuesses(0).WithGameStatus(GameStatus.Won).Build(),
            new Builders.GameBuilder().WithGameId(42).WithPlayerId(5).WithSecretWord("abcd").WithRightGuesses(4).WithWrongGuesses(3).WithGameStatus(GameStatus.Won).Build(),

            new Builders.GameBuilder().WithGameId(43).WithPlayerId(5).WithSecretWord("efgh").WithRightGuesses(2).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(44).WithPlayerId(5).WithSecretWord("efgh").WithRightGuesses(0).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),

            new Builders.GameBuilder().WithGameId(45).WithPlayerId(6).WithSecretWord("efgh").WithRightGuesses(4).WithWrongGuesses(0).WithGameStatus(GameStatus.Won).Build(),
            new Builders.GameBuilder().WithGameId(46).WithPlayerId(6).WithSecretWord("abcd").WithRightGuesses(4).WithWrongGuesses(3).WithGameStatus(GameStatus.Won).Build(),

            new Builders.GameBuilder().WithGameId(47).WithPlayerId(6).WithSecretWord("efgh").WithRightGuesses(0).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(48).WithPlayerId(6).WithSecretWord("abcd").WithRightGuesses(1).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(49).WithPlayerId(6).WithSecretWord("efgh").WithRightGuesses(2).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(50).WithPlayerId(6).WithSecretWord("efgh").WithRightGuesses(0).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(51).WithPlayerId(6).WithSecretWord("abcd").WithRightGuesses(1).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(52).WithPlayerId(6).WithSecretWord("efgh").WithRightGuesses(2).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(53).WithPlayerId(6).WithSecretWord("efgh").WithRightGuesses(0).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build(),
            new Builders.GameBuilder().WithGameId(54).WithPlayerId(6).WithSecretWord("abcd").WithRightGuesses(1).WithWrongGuesses(6).WithGameStatus(GameStatus.Lost).Build()
        }.AsQueryable();

        mockPlayersRepo.Setup(repo => repo.GetPlayersAsync()).ReturnsAsync(players);
        mockGamesRepo.Setup(repo => repo.GetGamesAsync()).ReturnsAsync(games);
    }

    [Fact]
    public async Task GetBestPlayersByGuessedWords_ShouldReturnStatisticsFor10PlayersWithMostGuessedWords() {
        List<PlayerStats> expected = new List<PlayerStats>() {
            new (){ PlayerId = 1, PlayerName = "player1", GuessedWords = 10, NotGuessedWords = 5, PercentGuessed = 66.7 },
            new (){ PlayerId = 2, PlayerName = "player2", GuessedWords = 5, NotGuessedWords = 3, PercentGuessed = 62.5 },
            new (){ PlayerId = 3, PlayerName = "player3", GuessedWords = 5, NotGuessedWords = 10, PercentGuessed = 33.3 },
            new (){ PlayerId = 5, PlayerName = "player5", GuessedWords = 2, NotGuessedWords = 2, PercentGuessed = 50.0 },
            new (){ PlayerId = 6, PlayerName = "player6", GuessedWords = 2, NotGuessedWords = 8, PercentGuessed = 20.0},
            new (){ PlayerId = 4, PlayerName = "player4", GuessedWords = 1, NotGuessedWords = 2, PercentGuessed = 33.3 }
        };

        IEnumerable<PlayerStats> actual = await sut.GetPlayersWithMostGuessedWordsAsync();

        Assert.Collection(actual,
            item => Assert.Equal(expected[0], item),
            item => Assert.Equal(expected[1], item),
            item => Assert.Equal(expected[2], item),
            item => Assert.Equal(expected[3], item),
            item => Assert.Equal(expected[4], item),
            item => Assert.Equal(expected[5], item));
    }
    [Fact]
    public async Task GetBestPlayersByRatio_ShouldReturnStatisticsFor10PlayersWithBestGuessedDividedByNonGuessedWords() {
        List<PlayerStats> expected = new List<PlayerStats>() {
            new (){ PlayerId = 1, PlayerName = "player1", GuessedWords = 10, NotGuessedWords = 5, PercentGuessed = 66.7 },
            new (){ PlayerId = 2, PlayerName = "player2", GuessedWords = 5, NotGuessedWords = 3, PercentGuessed = 62.5 },
            new (){ PlayerId = 5, PlayerName = "player5", GuessedWords = 2, NotGuessedWords = 2, PercentGuessed = 50.0 },
            new (){ PlayerId = 3, PlayerName = "player3", GuessedWords = 5, NotGuessedWords = 10, PercentGuessed = 33.3 },
            new (){ PlayerId = 4, PlayerName = "player4", GuessedWords = 1, NotGuessedWords = 2, PercentGuessed = 33.3 },
            new (){ PlayerId = 6, PlayerName = "player6", GuessedWords = 2, NotGuessedWords = 8, PercentGuessed = 20.0}
        };

        IEnumerable<PlayerStats> actual = await sut.GetPlayersWithBestPercentGuessedAsync();

        Assert.Collection(actual,
            item => Assert.Equal(expected[0], item),
            item => Assert.Equal(expected[1], item),
            item => Assert.Equal(expected[2], item),
            item => Assert.Equal(expected[3], item),
            item => Assert.Equal(expected[4], item),
            item => Assert.Equal(expected[5], item));
    }
    [Fact]
    public void GetPlayerStatistics_ShouldReturnPlayerStats_WhenPlayerExists() {

    }
    [Fact]
    public void GetPlayerStatistics_ShouldReturnNull_WhenPlayerDoesNotExist() {

    }
}
