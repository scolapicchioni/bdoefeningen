namespace HangMan.Core.Interfaces;
public interface IWordSelector {
    Task<string> SelectWord();
}
