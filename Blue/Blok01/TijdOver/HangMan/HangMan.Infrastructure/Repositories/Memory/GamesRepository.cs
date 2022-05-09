using HangMan.Core;
using HangMan.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMan.Infrastructure.Repositories.Memory {
    public class GamesRepository : IGamesRepository {
        private List<Game> games ;
        public GamesRepository() {
            games = new() {
                new() { Id = 1, PlayerId = 1, SecretWord = "art", Guesses = new SortedSet<Guess>() { 
                        new ('a',true), new ('r',true),new ('t',true),new ('b',false),new ('n',false)
                    } 
                },
                new() {
                    Id = 2, PlayerId = 1, SecretWord = "but", Guesses = new SortedSet<Guess>() {
                        new ('a',false), new ('r',false),new ('t',true),new ('b',true),new ('n',false), new ('c', false), new ('d', false), new ('e', false)
                    }
                },
                new() {
                    Id = 3, PlayerId = 1, SecretWord = "ice", Guesses = new SortedSet<Guess>() {
                        new ('a',false), new ('r',false),new ('t',false),new ('b',false),new ('n',false), new ('l', false)
                    }
                },
                new() {
                    Id = 4, PlayerId = 2, SecretWord = "art", Guesses = new SortedSet<Guess>() {
                        new ('a',true), new ('r',true),new ('t',true),new ('b',false),new ('n',false), new ('z', false), new('k', false)
                    }
                },
                new() {
                    Id = 5, PlayerId = 2, SecretWord = "but", Guesses = new SortedSet<Guess>() {
                        new ('a',false), new ('r',false),new ('t',true),new ('b',true),new ('n',false), new ('c', false), new ('d', false), new ('e', false)
                    }
                },
                new() {
                    Id = 6, PlayerId = 2, SecretWord = "ice", Guesses = new SortedSet<Guess>() {
                        new ('a',false), new ('r',false),new ('t',false),new ('b',false),new ('n',false), new ('l', false), new('i', true)
                    }
                },
                new() {
                    Id = 7, PlayerId = 3, SecretWord = "art", Guesses = new SortedSet<Guess>() {
                        new ('a',true), new ('r',true),new ('t',true),new ('b',false),new ('n',false), new ('z', false), new('k', false)
                    }
                },
                new() {
                    Id = 8, PlayerId = 3, SecretWord = "but", Guesses = new SortedSet<Guess>() {
                        new ('a',false), new ('r',false),new ('t',true),new ('b',true),new ('n',false), new ('c', false), new ('d', false), new ('e', false)
                    }
                },
                new() {
                    Id = 9, PlayerId = 3, SecretWord = "ice", Guesses = new SortedSet<Guess>() {
                        new ('a',false), new ('r',false),new ('t',false),new ('b',false),new ('n',false), new ('l', false), new('i', true)
                    }
                },
                new() {
                    Id = 10, PlayerId = 3, SecretWord = "art", Guesses = new SortedSet<Guess>() {
                        new ('a',true), new ('r',true),new ('t',true),new ('b',false),new ('n',false), new ('z', false), new('k', false)
                    }
                },
                new() {
                    Id = 11, PlayerId = 3, SecretWord = "but", Guesses = new SortedSet<Guess>() {
                        new ('a',false), new ('r',false),new ('t',true),new ('b',true),new ('n',false), new ('c', false), new ('d', false), new ('e', false)
                    }
                },
                new() {
                    Id = 12, PlayerId = 3, SecretWord = "ice", Guesses = new SortedSet<Guess>() {
                        new ('a',false), new ('r',false),new ('t',false),new ('b',false),new ('n',false), new ('l', false), new('i', true)
                    }
                },
                new() {
                    Id = 13, PlayerId = 4, SecretWord = "art", Guesses = new SortedSet<Guess>() {
                        new ('a',true), new ('r',true),new ('t',true),new ('b',false),new ('n',false), new ('z', false), new('k', false)
                    }
                }
            };
        }
        public Task<Game> AddGameAsync(Game game) {
            game.Id = games.Count==0 ? 1 : games.Max(g=>g.Id) + 1;
            games.Add(game);
            return Task.FromResult(game);
        }

        public Task<Game> GetGameByIdAsync(int id) {
            return Task.FromResult(games.FirstOrDefault(g => g.Id == id));
        }

        public Task<IQueryable<Game>> GetGamesAsync() {
            return Task.FromResult(games.AsQueryable());
        }

        public Task<Game> UpdateGameAsync(Game game) {
            Game oldGame = games.FirstOrDefault(g => g.Id == game.Id);
            oldGame.Guesses = game.Guesses;
            return Task.FromResult(oldGame);
        }
    }
}
