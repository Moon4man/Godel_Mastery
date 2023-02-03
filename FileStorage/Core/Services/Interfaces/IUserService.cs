namespace FileStorage.Core.Services.Interfaces
{
    public interface IUserService
    {
        string Name { get; }
        string CreationDate { get; }
        void GetInfo();
    }
}
