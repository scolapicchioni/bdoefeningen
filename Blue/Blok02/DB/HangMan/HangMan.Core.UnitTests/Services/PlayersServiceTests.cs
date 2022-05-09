using Xunit;
using Moq;
using HangMan.Core.Interfaces;
using HangMan.Core.Entities;
using HangMan.Core.Services;

namespace HangMan.Core.UnitTests.Services;
public class PlayersServiceTests {
    Mock<IPlayersRepository> mockPlayersRepo;
    PlayersService sut;
    public PlayersServiceTests() {
        mockPlayersRepo = new();
        sut = new(mockPlayersRepo.Object);
    }
    [Fact]
    public async Task NewPlayer_ShouldAddPlayer_WhenPlayerWithSameNameDoesNotExist() {
        string playerName = "player1";
        Player playerToAdd = new Player() { Id = 0, Name = playerName };
        Player expected = new Player() { Id = 1, Name = playerName };
        mockPlayersRepo.Setup(x => x.AddPlayerAsync(playerToAdd)).ReturnsAsync(expected);

        Player? actual = await sut.AddPlayer(playerToAdd);

        Assert.Equal(expected, actual);
    }
    [Fact]
    public async Task NewPlayer_ShouldThrow_WhenPlayerWithSameNameAlreadyExists() {
        string playerName = "player1";
        Player playerToAdd = new Player() { Id = 0, Name = playerName };
        mockPlayersRepo.Setup(x => x.AddPlayerAsync(playerToAdd)).ReturnsAsync(default(Player));

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await sut.AddPlayer(playerToAdd));

    }
    [Fact]
    public async Task GetPlayerByName_ShouldReturnPlayer_WhenPlayerExists() {
        string playerName = "player1";
        Player expected = new Player() { Id = 1, Name = playerName };
        mockPlayersRepo.Setup(x => x.GetPlayerByNameAsync(playerName)).ReturnsAsync(expected);

        Player? actual = await sut.GetPlayerByName(playerName);

        Assert.Equal(expected, actual);
    }
    [Fact]
    public async Task GetPlayerByName_ShouldReturnNull_WhenPlayerDoesNotExist() {
        string playerName = "player1";
        mockPlayersRepo.Setup(x => x.GetPlayerByNameAsync(playerName)).ReturnsAsync(default(Player));

        Player? actual = await sut.GetPlayerByName(playerName);

        Assert.Null(actual);
    }
    [Fact]
    public async Task GetPlayerById_ShouldReturnPlayer_WhenPlayerExists() {
        int playerId = 1;
        Player expected = new Player() { Id = playerId, Name = "playerName" };
        mockPlayersRepo.Setup(x => x.GetPlayerByIdAsync(playerId)).ReturnsAsync(expected);

        Player? actual = await sut.GetPlayerById(playerId);

        Assert.Equal(expected, actual);
    }
    [Fact]
    public async Task GetPlayerById_ShouldReturnNull_WhenPlayerDoesNotExist() {
        int playerId = 1;

        mockPlayersRepo.Setup(x => x.GetPlayerByIdAsync(playerId)).ReturnsAsync(default(Player));

        Player? actual = await sut.GetPlayerById(playerId);

        Assert.Null(actual);
    }
    [Fact]
    public async Task GetPlayersAsync_ShouldReturnRangeOfPlayers_InAlphabeticalOrder() {
        IQueryable<Player> players = new List<Player>() {
            new () { Id = 1, Name = "Mario"},
            new () { Id = 2, Name = "Andrea"},
            new () { Id = 3, Name = "Bob"},
            new () { Id = 4, Name = "Zoe"},
            new () { Id = 5, Name = "Walter"},
            new () { Id = 6, Name = "Claire"},
            new () { Id = 7, Name = "Xavier"},
            new () { Id = 8, Name = "Daisy"},
            new () { Id = 9, Name = "Ymir"},
            new () { Id = 10, Name = "Ellen"},
            new () { Id = 11, Name = "Vivian"},
            new () { Id = 12, Name = "Frank"},
        }.AsQueryable();
        mockPlayersRepo.Setup(x => x.GetPlayersAsync()).ReturnsAsync(players);
        List<Player> expected = new List<Player>() {
            //new () { Id = 2, Name = "Andrea"},
            //new () { Id = 3, Name = "Bob"},
            //new () { Id = 6, Name = "Claire"},
            //new () { Id = 8, Name = "Daisy"},
            //new () { Id = 10, Name = "Ellen"},
            //new () { Id = 12, Name = "Frank"},
            //new () { Id = 1, Name = "Mario"},
            //new () { Id = 11, Name = "Vivian"},
            new () { Id = 5, Name = "Walter"},
            new () { Id = 7, Name = "Xavier"},
            //new () { Id = 9, Name = "Ymir"},
            //new () { Id = 4, Name = "Zoe"},
        };
        IEnumerable<Player> actual = await sut.GetPlayersAsync(8..10);
        Assert.Collection(actual,
            item => Assert.Equal(expected[0].Id, item.Id),
            item => Assert.Equal(expected[1].Id, item.Id)

        );
    }
    [Fact]
    public async Task GetPlayersAsync_ShouldReturnRangeOfPlayersToEnd_WhenRangeHasNoEnd_InAlphabeticalOrder() {
        IQueryable<Player> players = new List<Player>() {
            new () { Id = 1, Name = "Mario"},
            new () { Id = 2, Name = "Andrea"},
            new () { Id = 3, Name = "Bob"},
            new () { Id = 4, Name = "Zoe"},
            new () { Id = 5, Name = "Walter"},
            new () { Id = 6, Name = "Claire"},
            new () { Id = 7, Name = "Xavier"},
            new () { Id = 8, Name = "Daisy"},
            new () { Id = 9, Name = "Ymir"},
            new () { Id = 10, Name = "Ellen"},
            new () { Id = 11, Name = "Vivian"},
            new () { Id = 12, Name = "Frank"},
        }.AsQueryable();
        mockPlayersRepo.Setup(x => x.GetPlayersAsync()).ReturnsAsync(players);
        List<Player> expected = new List<Player>() {
            //new () { Id = 2, Name = "Andrea"},
            //new () { Id = 3, Name = "Bob"},
            //new () { Id = 6, Name = "Claire"},
            //new () { Id = 8, Name = "Daisy"},
            //new () { Id = 10, Name = "Ellen"},
            //new () { Id = 12, Name = "Frank"},
            //new () { Id = 1, Name = "Mario"},
            //new () { Id = 11, Name = "Vivian"},
            new () { Id = 5, Name = "Walter"},
            new () { Id = 7, Name = "Xavier"},
            new () { Id = 9, Name = "Ymir"},
            new () { Id = 4, Name = "Zoe"},
        };
        IEnumerable<Player> actual = await sut.GetPlayersAsync(8..);
        Assert.Collection(actual,
            item => Assert.Equal(expected[0].Id, item.Id),
            item => Assert.Equal(expected[1].Id, item.Id),
            item => Assert.Equal(expected[2].Id, item.Id),
            item => Assert.Equal(expected[3].Id, item.Id)
        );
    }
}
