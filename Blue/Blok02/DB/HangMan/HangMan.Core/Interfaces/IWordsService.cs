namespace HangMan.Core.Interfaces; 
public interface IWordsService {
    Task<string> SelectWord();
}
