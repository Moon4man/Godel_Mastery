using FileStorage.Core.Services.Interfaces;
using System.Configuration;

namespace FileStorage.Core.Services
{
    public class StorageService : IStorageService
    {
        public StorageService() { }

        public void Initialize()
        {
            if (!Directory.Exists(StoragePath))
            {
                Directory.CreateDirectory(StoragePath);
                Console.WriteLine("\nStorage was successfully created");
            }
            else
            {
                Console.WriteLine("\nThis storage name is already exists. You can specify other in App.config");
            }
        }

        public long GetSizeStorageInMb()
        {
            long sizeStorage = 0;
            if (!Directory.Exists(StoragePath))
            {
                return sizeStorage;
            }

            sizeStorage = Directory.EnumerateFiles(StoragePath, "*", new EnumerationOptions { RecurseSubdirectories = true })
                .Sum(fileInfo => fileInfo.Length);

            return sizeStorage / (1024 * 1024);
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
