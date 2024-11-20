using Microsoft.AspNetCore.Mvc;
using TalkOTC.Services.Interfaces;

namespace TalkOTC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IActionResult> Login(string ReturnUrl)
        {
            await _accountService.SignInAsync();
            return Redirect(ReturnUrl);
        }
    }
}
