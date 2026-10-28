using EscapeRoomWeb.Models;
using EscapeRoomWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EscapeRoomWeb.Pages;

public class GameModel : PageModel
{
    private readonly GameService _game;
    public Puzzle? Puzzle { get; set; }
    public int RoomNumber { get; set; }
    public int TotalRooms { get; set; }
    public string Message { get; set; } = "";
    public bool Completed { get; set; }
    public bool IsLoggedIn => HttpContext.Session.GetInt32("UserId") != null;
    public int Score { get; set; }
    public int Attempts { get; set; }
    public int SecondsTaken { get; set; }
    public long StartUnixMilliseconds { get; set; }

    public GameModel(GameService game) => _game=game;

    public async Task<IActionResult> OnGetAsync()
    {
        if (!IsLoggedIn) return Page();
        if (HttpContext.Session.GetInt32("GameStart")==null)
            HttpContext.Session.SetInt32("GameStart",(int)DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        await LoadRoomAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string answer)
    {
        if (!IsLoggedIn) return RedirectToPage("/Account/Login");
        int room=HttpContext.Session.GetInt32("Room")??1;
        var puzzles=await _game.GetPuzzlesAsync();
        var puzzle=puzzles.FirstOrDefault(x=>x.RoomNumber==room);
        if(puzzle==null) return RedirectToPage();

        int attempts=(HttpContext.Session.GetInt32("Attempts")??0)+1;
        HttpContext.Session.SetInt32("Attempts",attempts);

        if(await _game.CheckAnswerAsync(puzzle.Id,answer))
        {
            int score=(HttpContext.Session.GetInt32("Score")??0)+puzzle.Points;
            HttpContext.Session.SetInt32("Score",score);

            if(room>=puzzles.Count)
            {
                int userId=HttpContext.Session.GetInt32("UserId")!.Value;
                int start=HttpContext.Session.GetInt32("GameStart")??(int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                int seconds=Math.Max(1,(int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()-start);
                await _game.SaveResultAsync(userId,score,attempts,seconds);
                Completed=true; Score=score; Attempts=attempts; SecondsTaken=seconds;
                HttpContext.Session.Clear();
                return Page();
            }
            HttpContext.Session.SetInt32("Room",room+1);
            Message="Correct. The next room is now unlocked.";
        }
        else Message="That answer does not open the lock. Read the clue again.";

        await LoadRoomAsync();
        return Page();
    }

    private async Task LoadRoomAsync()
    {
        var puzzles=await _game.GetPuzzlesAsync();
        TotalRooms=puzzles.Count;
        RoomNumber=HttpContext.Session.GetInt32("Room")??1;
        Puzzle=puzzles.FirstOrDefault(x=>x.RoomNumber==RoomNumber);
        int start=HttpContext.Session.GetInt32("GameStart")??(int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        StartUnixMilliseconds=DateTimeOffset.FromUnixTimeSeconds(start).ToUnixTimeMilliseconds();
    }
}