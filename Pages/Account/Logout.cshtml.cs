using Microsoft.AspNetCore.Mvc.RazorPages;
namespace EscapeRoomWeb.Pages.Account;
public class LogoutModel:PageModel{public void OnGet()=>HttpContext.Session.Clear();}