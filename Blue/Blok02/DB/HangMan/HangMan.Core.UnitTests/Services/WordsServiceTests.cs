using Xunit;
using Moq;
using HangMan.Core.Interfaces;
using HangMan.Core.Services;

namespace HangMan.Core.UnitTests.Services;
public class WordsServiceTests {
    [Fact]
    public async Task SelectWord_ReturnsWord() {
        IQueryable<string> words = new List<string>() {
            "able", "about", "account","acid","across"
        }.AsQueryable();
        Mock<IWordsRepository> mockWordsRepository = new();

        mockWordsRepository.Setup(w => w.GetWordsAsync()).ReturnsAsync(words);
        WordsService sut = new WordsService(mockWordsRepository.Object);

        string word = await sut.SelectWord();

        Assert.NotNull(word);
        Assert.NotEmpty(word);
        Assert.Contains(word, words);
    }
}
