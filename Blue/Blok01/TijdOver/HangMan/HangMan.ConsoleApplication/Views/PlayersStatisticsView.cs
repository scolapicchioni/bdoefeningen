using HangMan.ConsoleApplication.Interfaces;
using HangMan.Core.Entities;

namespace HangMan.ConsoleApplication.Views;
public class PlayersStatisticsView : IPlayersStatisticsView {
    public IEnumerable<PlayerStats> PlayersStats { get => throw new NotImplementedException(); 
        set {
            Console.WriteLine($"PlayerId\tPlayer Name\tGuessed\tNot Guessed\tPercent Guessed");
            foreach (var item in value) {
                Console.WriteLine($"{item.PlayerId}\t{item.PlayerName}\t{item.GuessedWords}\t{item.NotGuessedWords}\t{item.PercentGuessed}");
            }
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
        } 
    }
    public string Title { get => throw new NotImplementedException(); set => Console.WriteLine(value); }

    public int Choice() {
        char userInput;
        int choice = -1;
        do {
            Console.Clear();
            Console.WriteLine("1. Players With Most Guessed Words");
            Console.WriteLine("2. Players With Best Percent Guessed");
            Console.WriteLine("0. Main Menu");
            userInput = Console.ReadKey().KeyChar;
            if (char.IsDigit(userInput))
                choice = int.Parse(userInput.ToString());
        } while( choice < 0 || choice > 2);
        return choice;
    }

    public void Show() {
        Console.Clear();
    }
}
