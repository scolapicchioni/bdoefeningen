using HangMan.ConsoleApplication.Interfaces;
using HangMan.Core.Entities;

namespace HangMan.ConsoleApplication.Views;
public class PlayersView : IPlayersView {
    public string Name { get { Console.WriteLine("Player's Name: "); return Console.ReadLine(); } }

    private Player player;
    public Player? Player { get => player; set => Console.WriteLine(player != null ? $"Selected Player: {player.Id} {player.Name}" : "No player has been selected"); }
    public Exception Error { 
        get => throw new NotImplementedException(); 
        set {
            Console.WriteLine(value.Message);
            Console.ReadLine();
        }
    }

    public int Choice() {
        char userInput;
        int choice = -1;
        do {
            Console.Clear();
            Console.WriteLine("1. Add Player");
            Console.WriteLine("2. Select Player by Name");
            Console.WriteLine("3. List All Players");
            Console.WriteLine("0. Return to Main Menu");
            userInput = Console.ReadKey().KeyChar;
            if (char.IsDigit(userInput))
                choice = int.Parse(userInput.ToString());
        } while (choice < 0 || choice > 3);
        return choice;
    }

    public void ListPlayers(IEnumerable<Player> players) {
        Console.WriteLine("Id - Name");
        foreach (var p in players) {
            Console.WriteLine($"{p.Id} {p.Name}");
        }
        Console.ReadLine();
    }
}
