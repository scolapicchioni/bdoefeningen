using HangMan.Core.Entities;

namespace HangMan.ConsoleApplication.Interfaces; 
public interface IMainView {
    int SelectFeature();
    void PlayerSelected(Player player);
    void Clear();
}
