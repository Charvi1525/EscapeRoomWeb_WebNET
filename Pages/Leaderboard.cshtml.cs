using EscapeRoomWeb.Models;
using EscapeRoomWeb.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace EscapeRoomWeb.Pages;
public class LeaderboardModel : PageModel
{
    private readonly GameService _game;
    public List<GameResult> Results { get; set; } = new();
    public LeaderboardModel(GameService game)=>_game=game;
    public async Task OnGetAsync()=>Results=await _game.GetLeaderboardAsync();
}