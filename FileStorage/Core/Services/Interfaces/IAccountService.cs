namespace FileStorage.Core.Services.Interfaces
{
    public interface IAccountService
    {
        bool GetAuthenticated(string login, string password);
    }
}
