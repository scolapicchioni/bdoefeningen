using HangMan.Core.Interfaces;
using HangMan.Infrastructure.Data;

namespace HangMan.Infrastructure.Repositories.EF;
public class WordsRepository : IWordsRepository {
    private readonly HangManDbContext context;
    public WordsRepository(HangManDbContext context) {
        this.context = context;
    }
    public  Task<IQueryable<string>> GetWordsAsync() {
        return Task.FromResult(context.Words.OrderBy(w => w.Value).Select(w => w.Value.ToLower()));
    }
}
