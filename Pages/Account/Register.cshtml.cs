using EscapeRoomWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace EscapeRoomWeb.Pages.Account;
public class RegisterModel:PageModel
{
    private readonly GameService _game;
    public RegisterModel(GameService game)=>_game=game;
    [BindProperty] public string Name{get;set;}="";
    [BindProperty] public string Email{get;set;}="";
    [BindProperty] public string Password{get;set;}="";
    public string Message{get;set;}="";
    public async Task<IActionResult> OnPostAsync()
    {
        if(Name.Trim().Length<2||Password.Length<6){Message="Enter a name and a password of at least 6 characters.";return Page();}
        if(await _game.FindUserAsync(Email)!=null){Message="An account with this email already exists.";return Page();}
        var user=await _game.CreateUserAsync(Name,Email,Password);
        HttpContext.Session.SetInt32("UserId",user.Id);
        HttpContext.Session.SetString("UserName",user.Name);
        HttpContext.Session.SetInt32("Room",1);
        HttpContext.Session.SetInt32("Score",0);
        HttpContext.Session.SetInt32("Attempts",0);
        HttpContext.Session.SetInt32("GameStart",(int)DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        return RedirectToPage("/Game");
    }
}