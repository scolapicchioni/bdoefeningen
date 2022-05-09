using HangMan.Infrastructure.Repositories.Files;
using Xunit;


namespace HangMan.Infrastructure.UnitTests.Repositories.Files;
public class WordsRepositoryTests {
    [Fact]
    public async Task SelectWord_ReturnsWord() {
        WordsRepository selector = new WordsRepository();

        IQueryable<string> words = await selector.GetWordsAsync();

        Assert.NotNull(words);
        Assert.NotEmpty(words);
    }
}
