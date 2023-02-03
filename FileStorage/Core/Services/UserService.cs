using FileStorage.Core.Services.Interfaces;
using System.Configuration;

namespace FileStorage.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IStorageService storageService;
        public UserService(IStorageService storageService)
        {
            this.storageService = storageService;
        }
        public UserService()
        {
            storageService = new StorageService();
        }

        public string Name => ConfigurationManager.AppSettings["login"];
        public string CreationDate => DateTime.Today.ToString("yyyy-MM-dd");

        public void GetInfo()
        {
            Console.WriteLine($"\nlogin: {Name}\n" +
                    $"creation Date: {CreationDate}\n" +
                    $"storage used: {storageService.GetSizeStorageInMb()} MB");
        }

        public string StoragePath
        {
            get
            {
                return ConfigurationManager.AppSettings["storage"];
            }
        }
    }
}
