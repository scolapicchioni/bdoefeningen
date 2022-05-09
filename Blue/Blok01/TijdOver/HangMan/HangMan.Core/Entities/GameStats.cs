namespace HangMan.Core.Entities {
    public class GameStats {
        public int Id { get; set; }
        public string SecretWord { get; set; }
        public int TotalAttempts { get; set; }
        public int FaultedAttempts { get; set; }
        public GameStatus Status { get; set; }
        public int PlayerId { get; set; }
    }
}