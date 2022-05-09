using HangMan.Core.Interfaces;

namespace HangMan.Core.Services;
public class WordsService : IWordsService {
    private IWordsRepository wordsRepository;
    private List<string> words;
    private Random random;

    public WordsService(IWordsRepository wordsRepository) {
        this.wordsRepository = wordsRepository;
        random = new Random();
    }

    public async Task<string> SelectWord() {
        if (words == null) {
            words = (await wordsRepository.GetWordsAsync()).Select(w => w.ToLower()).Distinct().ToList();
        }

        return words[random.Next(0, words.Count)];
    }
}
