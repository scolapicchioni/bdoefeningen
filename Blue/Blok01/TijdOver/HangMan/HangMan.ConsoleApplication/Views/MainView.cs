using HangMan.ConsoleApplication.Interfaces;
using HangMan.Core.Entities;

namespace HangMan.ConsoleApplication.Views;
public class MainView : IMainView {
    public void PlayerSelected(Player player) {
        if(player==null)
            Console.WriteLine("No Player Selected. Please select a player.");
        else
            Console.WriteLine($"Selected Player: {player.Id} - {player.Name}");
        Console.ReadLine();
    }

    public int SelectFeature() {
        char userInput;
        int choice = -1;
        do {
            Console.WriteLine("1. Select Player");
            Console.WriteLine("2. Start Game");
            Console.WriteLine("3. Game Statistics");
            Console.WriteLine("4. Players Statistics");
            Console.WriteLine("0. Exit");
            userInput = Console.ReadKey().KeyChar;
            if (char.IsDigit(userInput))
                choice = int.Parse(userInput.ToString());
        } while (choice < 0 || choice > 4);
        return choice;
    }
    public void Clear() {
        Console.Clear();
    }
}
