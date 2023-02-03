using FileStorage.Core.Services.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Configuration;

namespace FileStorage.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IStorageService storageService;
        public AccountService(IStorageService storageService)
        {
            this.storageService = storageService;
        }
        public AccountService()
        {
            storageService = new StorageService();
        }

        public bool GetAuthenticated(string login, string password)
        {
            var salt = Convert.FromBase64String(ConfigurationManager.AppSettings["saltPassword"]);
            var hashString = Convert.ToBase64String(CalculateHash(password, salt));
            if (ConfigurationManager.AppSettings["login"] == login && ConfigurationManager.AppSettings["hashPassword"] == hashString)
            {
                storageService.Initialize();
                return true;
            }
            return false;
        }

        private byte[] CalculateHash(string password, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);
        }
    }
}
