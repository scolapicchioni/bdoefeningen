namespace HangMan.Core.Entities;
public class Player {
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Game> Games { get; set; }
}
