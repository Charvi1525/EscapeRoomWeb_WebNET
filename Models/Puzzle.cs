namespace EscapeRoomWeb.Models;

public class Puzzle
{
    public int Id { get; set; }
    public int RoomNumber { get; set; }
    public string RoomTitle { get; set; } = "";
    public string Story { get; set; } = "";
    public string Clue { get; set; } = "";
    public string Question { get; set; } = "";
    public string Answer { get; set; } = "";
    public int Points { get; set; }
}