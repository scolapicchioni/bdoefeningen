namespace HangMan.Core; 
public interface IWordSelector {
    Task<string> SelectWord();
}
