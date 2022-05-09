using HangMan.Core;

namespace HangMan.Infrastructure; 
public class WordFileSelector : IWordSelector {
    public List<string> Words { get; set; }
    private Random random ;
    public WordFileSelector() {
        random = new Random();
    }
    public async Task<string> SelectWord() {
        if (Words == null) {
            Words = (await File.ReadAllLinesAsync(Path.Combine(Directory.GetCurrentDirectory(),"words.txt"))).Select(w=>w.ToLower()).Distinct().ToList();
        }

        return Words[random.Next(0, Words.Count)];
    }
}
