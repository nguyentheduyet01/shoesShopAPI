namespace ShoesSopAPI.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Account> Login(string username, string password);

    }
}
