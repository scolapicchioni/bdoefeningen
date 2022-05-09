using HangMan.ConsoleApplication.Interfaces;
using HangMan.Core.Entities;

namespace HangMan.ConsoleApplication.Views;
public class GameStatisticsView : IGameStatisticsView {
    
    public IEnumerable<Game> Games { get; set; }

    public void ShowGamesStatistics() {
        Console.Clear();
        Console.WriteLine("| Id |         Word       |  Status    |Total|Right|Wrong|");
        Console.WriteLine("|----|--------------------|------------|-----|-----|-----|");
        foreach (Game game in Games) {
            Console.WriteLine($"|{game.Id,4}|{game.SecretWord,-20}|{game.Status,-12}|{game.Guesses.Count(),5}|{game.RightGuesses.Count(),5}|{game.WrongGuesses.Count(),5}|");
        }
        Console.WriteLine("Press Enter to Continue.");
        Console.ReadLine();
    }
}
