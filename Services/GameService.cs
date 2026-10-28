using EscapeRoomWeb.Data;
using EscapeRoomWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace EscapeRoomWeb.Services;

public class GameService
{
    private readonly GameDbContext _db;
    public GameService(GameDbContext db) => _db = db;

    public Task<List<Puzzle>> GetPuzzlesAsync() =>
        _db.Puzzles.OrderBy(x => x.RoomNumber).ToListAsync();

    public Task<User?> FindUserAsync(string email) =>
        _db.Users.FirstOrDefaultAsync(x => x.Email == email);

    public async Task<User> CreateUserAsync(string name, string email, string password)
    {
        var user = new User {
            Name=name.Trim(),
            Email=email.Trim().ToLowerInvariant(),
            PasswordHash=BCrypt.Net.BCrypt.HashPassword(password)
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<bool> CheckAnswerAsync(int id, string answer)
    {
        var puzzle = await _db.Puzzles.FindAsync(id);
        return puzzle != null &&
            puzzle.Answer.Equals(answer.Trim(), StringComparison.OrdinalIgnoreCase);
    }

    public async Task SaveResultAsync(int userId, int score, int attempts, int seconds)
    {
        _db.GameResults.Add(new GameResult {
            UserId=userId, Score=score, Attempts=attempts, SecondsTaken=seconds
        });
        await _db.SaveChangesAsync();
    }

    public Task<List<GameResult>> GetLeaderboardAsync() =>
        _db.GameResults.Include(x => x.User)
            .OrderByDescending(x => x.Score)
            .ThenBy(x => x.SecondsTaken)
            .Take(10).ToListAsync();
}