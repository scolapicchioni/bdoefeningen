namespace HangMan.ConsoleApplication.Interfaces;
public interface IGamePresenter {
    Task Show();
    int PlayerId { get; set; }
}
