using EscapeRoomWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace EscapeRoomWeb.Pages.Account;
public class LoginModel:PageModel
{
    private readonly GameService _game;
    public LoginModel(GameService game)=>_game=game;
    [BindProperty] public string Email{get;set;}="";
    [BindProperty] public string Password{get;set;}="";
    public string Message{get;set;}="";
    public async Task<IActionResult> OnPostAsync()
    {
        var user=await _game.FindUserAsync(Email);
        if(user==null||!BCrypt.Net.BCrypt.Verify(Password,user.PasswordHash))
        {Message="Email or password is incorrect.";return Page();}
        HttpContext.Session.SetInt32("UserId",user.Id);
        HttpContext.Session.SetString("UserName",user.Name);
        HttpContext.Session.SetInt32("Room",1);
        HttpContext.Session.SetInt32("Score",0);
        HttpContext.Session.SetInt32("Attempts",0);
        HttpContext.Session.SetInt32("GameStart",(int)DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        return RedirectToPage("/Game");
    }
}