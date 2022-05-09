using HangMan.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HangMan.Infrastructure.Data; 
public class HangManDbContext : DbContext {
    public HangManDbContext(DbContextOptions<HangManDbContext> options) : base(options) {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        ConfigurePlayer(modelBuilder.Entity<Player>());

        ConfigureWord(modelBuilder.Entity<Word>());

        ConfigureGuess(modelBuilder.Entity<Guess>());

        ConfigureGame(modelBuilder.Entity<Game>());

    }

    private void ConfigureGuess(EntityTypeBuilder<Guess> builder) {
        builder.HasKey(x => x.Id);
        builder.HasOne(gu => gu.Game).WithMany(ga => ga.Guesses);

        List<Guess> guesses = new List<Guess>() { 
            new Guess('a',true){Id = 1, GameId = 1},
            new Guess('b',true){Id = 2, GameId = 1},
            new Guess('l',true){Id = 3, GameId = 1},
            new Guess('e',true){Id = 4, GameId = 1},
            new Guess('a',true){Id = 5, GameId = 2},
            new Guess('r',false){Id = 6, GameId = 2},
            new Guess('t',false){Id = 7, GameId = 2},
            new Guess('l',false){Id = 8, GameId = 2},
            new Guess('n',false){Id = 9, GameId = 2},
            new Guess('m',false){Id = 10, GameId = 2},
            new Guess('o',false){Id = 11, GameId = 2},
            new Guess('c',true){Id = 12, GameId = 3},
            new Guess('a',true){Id = 13, GameId = 3},
            new Guess('k',true){Id = 14, GameId = 3},
            new Guess('e',true){Id = 15, GameId = 3},
            new Guess('a',true){Id = 16, GameId = 4},
            new Guess('e',true){Id = 17, GameId = 5},
            new Guess('a',true){Id = 18, GameId = 5},
            new Guess('r',true){Id = 19, GameId = 5},
            new Guess('t',false){Id = 20, GameId = 5},
            new Guess('f',true){Id = 21, GameId = 6},
            new Guess('a',true){Id = 22, GameId = 6},
            new Guess('c',true){Id = 23, GameId = 6},
            new Guess('e',true){Id = 24, GameId = 6},
            new Guess('b',false){Id = 25, GameId = 7},
            new Guess('c',false){Id = 26, GameId = 7},
            new Guess('f',false){Id = 27, GameId = 7},
            new Guess('h',false){Id = 28, GameId = 7},
            new Guess('i',false){Id = 29, GameId = 7},
            new Guess('j',false){Id = 30, GameId = 7},
            new Guess('h',true){Id = 31, GameId = 8},
            new Guess('a',true){Id = 32, GameId = 8},
            new Guess('i',true){Id = 33, GameId = 8},
            new Guess('r',true){Id = 34, GameId = 8},
            new Guess('i',true){Id = 35, GameId = 9},
            new Guess('j',true){Id = 36, GameId = 10},
            new Guess('e',true){Id = 37, GameId = 10},
            new Guess('l',true){Id = 38, GameId = 10},
            new Guess('y',true){Id = 39, GameId = 10}
        };
        builder.HasData(guesses);
    }

    private void ConfigureGame(EntityTypeBuilder<Game> builder) {
        builder.HasKey(x => x.Id);
        builder.Property(g=>g.SecretWord).HasMaxLength(100).IsRequired();
        builder.Ignore(g => g.WrongGuesses);
        builder.Ignore(g => g.RightGuesses);
        builder.Ignore(g => g.GuessedWord);
        builder.HasOne(g => g.Player).WithMany(p => p.Games);

        List<Game> games = new List<Game>() {
            new(){ Id = 1, SecretWord = "able", PlayerId = 1, Status = GameStatus.Won },
            new(){ Id = 2, SecretWord = "baby", PlayerId = 1, Status = GameStatus.Lost },
            new(){ Id = 3, SecretWord = "cake", PlayerId = 1, Status = GameStatus.Won },
            new(){ Id = 4, SecretWord = "damage", PlayerId = 1, Status = GameStatus.InProgress },
            new(){ Id = 5, SecretWord = "ear", PlayerId = 2, Status = GameStatus.Won },
            new(){ Id = 6, SecretWord = "face", PlayerId = 2, Status = GameStatus.Won },
            new(){ Id = 7, SecretWord = "garden", PlayerId = 3, Status = GameStatus.Lost },
            new(){ Id = 8, SecretWord = "hair", PlayerId = 3, Status = GameStatus.Won },
            new(){ Id = 9, SecretWord = "ice", PlayerId = 3, Status = GameStatus.InProgress },
            new(){ Id = 10, SecretWord = "jelly", PlayerId = 4, Status = GameStatus.Won }
        };
        builder.HasData(games);
    }

    private void ConfigurePlayer(EntityTypeBuilder<Player> builder) {
        builder.HasKey(x => x.Id);
        builder.HasIndex(p => p.Name).IsUnique();
        builder.Property(p => p.Name).HasMaxLength(200).IsRequired();
    
        List<Player> players = new List<Player>() {
            new(){Id = 1, Name = "Mario"},
            new(){Id = 2, Name = "Luigi"},
            new(){Id = 3, Name = "Peach"},
            new(){Id = 4, Name = "Daisy"}
        };
        builder.HasData(players);
    }

    private void ConfigureWord(EntityTypeBuilder<Word> builder) {
        builder.HasKey(x => x.Id);
        builder.HasIndex(p => p.Value).IsUnique();
        builder.Property(p => p.Value).HasMaxLength(200).IsRequired();

        List<Word> Words = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "words.txt")).Distinct().Select((w, i) => new Word { Id = i+1, Value = w.ToLower() }).Distinct().ToList();
        builder.HasData(Words);
    }

    public DbSet<Player> Players { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Guess> Guesses { get; set; }

    public DbSet<Word> Words { get; set; }
}
