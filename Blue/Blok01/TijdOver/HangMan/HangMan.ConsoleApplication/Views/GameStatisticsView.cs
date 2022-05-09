using HangMan.ConsoleApplication.Interfaces;
using HangMan.Core;

namespace HangMan.ConsoleApplication.Views;
public class GameStatisticsView : IGameStatisticsView {
    
    public IEnumerable<Game> Games { get; set; }

    public void ShowGamesStatistics() {
        Console.Clear();
        Console.WriteLine("Id\tWord\tGuessed\tTotal\tRight\tWrong");
        foreach (Game game in Games) {
            Console.WriteLine($"{game.Id}\t{game.SecretWord}\t{game.WordHasBeenGuessed}\t{game.Guesses.Count()}\t{game.RightGuesses.Count()}\t{game.WrongGuesses.Count()}");
        }
        Console.WriteLine("Press Enter to Continue.");
        Console.ReadLine();
    }
}
