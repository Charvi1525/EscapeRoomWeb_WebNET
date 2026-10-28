using EscapeRoomWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace EscapeRoomWeb.Data;

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }
    public DbSet<User> Users => Set<User>();
    public DbSet<Puzzle> Puzzles => Set<Puzzle>();
    public DbSet<GameResult> GameResults => Set<GameResult>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
        modelBuilder.Entity<GameResult>()
            .HasOne(x => x.User)
            .WithMany(x => x.Results)
            .HasForeignKey(x => x.UserId);
    }
}