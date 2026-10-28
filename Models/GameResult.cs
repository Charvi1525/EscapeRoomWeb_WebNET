namespace EscapeRoomWeb.Models;

public class GameResult
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public int Score { get; set; }
    public int Attempts { get; set; }
    public int SecondsTaken { get; set; }
    public DateTime CompletedAt { get; set; } = DateTime.Now;
}