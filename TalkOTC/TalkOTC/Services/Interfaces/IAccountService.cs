namespace TalkOTC.Services.Interfaces
{
    public interface IAccountService
    {
        Task SignInAsync();
        Task SignOutAsync();
    }
}
