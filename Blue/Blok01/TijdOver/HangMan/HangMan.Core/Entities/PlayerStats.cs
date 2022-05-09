namespace HangMan.Core.Entities;
public record PlayerStats {
    public int PlayerId { get; set; }
    public string PlayerName { get; set; }
    public int GuessedWords { get; set; }
    public int NotGuessedWords { get; set; }
    public double PercentGuessed { get; set; }
}
