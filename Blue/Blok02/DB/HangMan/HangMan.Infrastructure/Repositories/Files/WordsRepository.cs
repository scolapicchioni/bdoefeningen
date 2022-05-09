using HangMan.Core.Interfaces;

namespace HangMan.Infrastructure.Repositories.Files;
public class WordsRepository : IWordsRepository {
    public List<string> Words { get; set; }

    public async Task<IQueryable<string>> GetWordsAsync() {
        if (Words == null) {
            Words = (await File.ReadAllLinesAsync(Path.Combine(Directory.GetCurrentDirectory(), "words.txt"))).Select(w => w.ToLower()).Distinct().ToList();
        }
        return Words.AsQueryable();
    }
}
