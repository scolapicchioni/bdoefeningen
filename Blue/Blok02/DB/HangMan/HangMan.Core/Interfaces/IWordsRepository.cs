namespace HangMan.Core.Interfaces;
public interface IWordsRepository {
    Task<IQueryable<string>> GetWordsAsync();
}
