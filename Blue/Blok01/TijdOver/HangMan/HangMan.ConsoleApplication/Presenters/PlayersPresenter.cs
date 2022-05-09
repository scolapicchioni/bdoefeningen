using HangMan.ConsoleApplication.Interfaces;
using HangMan.Core.Entities;
using HangMan.Core.Interfaces;

namespace HangMan.ConsoleApplication.Presenters; 
public class PlayersPresenter : IPlayersPresenter {
    private readonly IPlayersView view;
    private readonly IPlayersService service;

    public PlayersPresenter(IPlayersView view, IPlayersService service) {
        this.view = view;
        this.service = service;
    }
    public async Task<Player> SelectPlayer() {
        Player player = null;
        string name;
        int choice;
        do {
            view.Player = player;
            choice = view.Choice();
            switch (choice) {
                case 1:
                    name = view.Name;
                    try {
                        player = await service.AddPlayer(new Player() { Name = name });
                    } catch (Exception ex) { 
                        view.Error = ex;
                    }
                    break;
                case 2:
                    name = view.Name;
                    player = await service.GetPlayerByName(name);
                    break;
                case 3:
                    view.ListPlayers(await service.GetPlayersAsync(0..));
                    break;
            }
        } while (choice != 0 && player == null);

        return player;
    }
}
