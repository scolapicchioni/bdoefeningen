using Xunit;

namespace HangMan.Infrastructure.UnitTests; 
public class WordSelectorTests {
    [Fact]
    public async Task SelectWord_ReturnsWord() {
        WordFileSelector selector = new WordFileSelector();

        string word = await selector.SelectWord();

        Assert.NotNull(word);
        Assert.NotEmpty(word);
    }
}
