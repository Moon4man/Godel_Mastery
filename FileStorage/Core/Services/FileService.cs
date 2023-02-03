using FileStorage.Core.Services.Interfaces;
using FileStorage.Infrastructure.Data.Models;
using FileStorage.Infrastructure.Repository;
using FileStorage.Infrastructure.Repository.Interfaces;
using System.Configuration;

namespace FileStorage.Core.Services
{
    public class FileService : IFileService
    {
        private readonly IMetaInformationRepository metaInformationRepository;
        private readonly IUserService userService;

        public FileService(IMetaInformationRepository metaInformationRepository, IUserService userService)
        {
            this.metaInformationRepository = metaInformationRepository;
            this.userService = userService;
        }
        public FileService()
        {
            metaInformationRepository = new MetaInformationRepository();
            userService = new UserService();
        }

        public void Download(string fileName, string path)
        {
            MetaInformation fileMetaInfo = metaInformationRepository.GetMetaFile(fileName);

            if (fileMetaInfo != null)
            {
                fileMetaInfo.DownloadNumber++;

                var isUploaded = metaInformationRepository.Update(fileMetaInfo, fileName);

                if (isUploaded)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    string storagePath = Path.Combine(StoragePath, fileName);
                    string newPath = Path.Combine(path, fileName);

                    if (File.Exists(newPath))
                    {
                        Console.WriteLine("\nFile alredy exists in directory");
                        return;
                    }

                    FileInfo fileInf = new FileInfo(storagePath);
                    if (fileInf.Exists)
                    {
                        fileInf.CopyTo(newPath, false);
                        Console.WriteLine($"\nThe file '{fileName}' has been downloaded");
                    }
                    else
                    {
                        Console.WriteLine("\nFile was not found in storage");
                    }
                }
            }
        }

        public void Find(string fileName)
        {
            foreach (string findFile in Directory.EnumerateFiles(StoragePath, fileName, SearchOption.AllDirectories))
            {
                FileInfo fileInf = new FileInfo(findFile);
                if (fileInf.Exists)
                {
                    Console.WriteLine($"\nname: {fileInf.Name}\n" +
                                      $"extension: {fileInf.Extension}\n" +
                                      $"file size: {fileInf.Length} byte\n" +
                                      $"creation date: {fileInf.CreationTime.ToString("yyyy-MM-dd")}\n" +
                                      $"last access time: {fileInf.LastAccessTime.ToString("yyyy-MM-dd")}");
                }
                else
                {
                    Console.WriteLine("\nFile was not found in storage");
                }
            }
        }

        public void GetInfo(string fileName)
        {

            MetaInformation fileMetaInfo = metaInformationRepository.GetMetaFile(fileName);

            if (fileMetaInfo != null)
            {
                Console.WriteLine($"\nname: {fileMetaInfo.FileName}\n" +
                                  $"extension: {fileMetaInfo.FileExtension}\n" +
                                  $"file size: {fileMetaInfo.FileSize} byte\n" +
                                  $"creation date: {fileMetaInfo.FileCreationDate}\n" +
                                  $"login: {userService.Name}\n" +
                                  $"last access time: {fileMetaInfo.LastAccessTime}\n" +
                                  $"number of dowlands: {fileMetaInfo.DownloadNumber}");
            }
            else
            {
                Console.WriteLine("\nFile was not found in storage");
            }
        }

        public void Move(string oldName, string newName)
        {

            if (metaInformationRepository.Exists(newName))
            {
                Console.WriteLine("\nFile with that name already exist in storage");
                return;
            }

            MetaInformation fileMetaInfo = metaInformationRepository.GetMetaFile(oldName);

            if (fileMetaInfo != null)
            {
                fileMetaInfo.FileName = newName;
                fileMetaInfo.FileExtension = Path.GetExtension(newName);

                var isUploaded = metaInformationRepository.Update(fileMetaInfo, oldName);

                if (isUploaded)
                {
                    string path = Path.Combine(StoragePath, oldName);
                    string newPath = Path.Combine(StoragePath, newName);

                    FileInfo fileInf = new FileInfo(path);
                    if (fileInf.Exists)
                    {
                        fileInf.MoveTo(newPath);
                        Console.WriteLine($"\nThe file '{oldName}' has been moved to '{newName}'");
                    }
                }
            }
            else
            {
                Console.WriteLine("\nFile was not found in storage");
            }
        }

        public void Remove(string fileName)
        {
            var isRemoved = metaInformationRepository.Remove(fileName);

            if (isRemoved)
            {
                string path = Path.Combine(StoragePath, fileName);

                FileInfo fileInf = new FileInfo(path);
                fileInf.Delete();
                Console.WriteLine($"\nThe file {fileName} has been removed");
            }
            else
            {
                Console.WriteLine("\nFile was not found in storage");
            }
        }

        public void Upload(string path)
        {
            if (metaInformationRepository.Exists(Path.GetFileName(path)))
            {
                Console.WriteLine("\nFile already exists!");
                return;
            }

            FileInfo fileInf = new FileInfo(path);
            var isUploaded = false;

            if (fileInf.Length < (150 * 1024 * 1024))
            {
                MetaInformation fileMetaInfo = new MetaInformation()
                {
                    FileName = fileInf.Name,
                    FileExtension = fileInf.Extension,
                    FileSize = fileInf.Length,
                    FileCreationDate = fileInf.CreationTime.ToString("yyyy-MM-dd"),
                    LastAccessTime = fileInf.LastAccessTime.ToString("yyyy-MM-dd"),
                    DownloadNumber = 0
                };

                isUploaded = metaInformationRepository.Upload(fileMetaInfo);
            }
            else
            {
                Console.WriteLine("\nThe file is too large!");
            }


            if (isUploaded)
            {
                string fileName = Path.GetFileName(path);
                string newPath = Path.Combine(StoragePath, fileName);

                fileInf.CopyTo(newPath, false);
                Console.WriteLine($"\nThe file '{newPath}' has been uploaded\n");
                Console.WriteLine($" - name: {fileInf.Name}\n" +
                                  $" - file size: {fileInf.Length} byte\n" +
                                  $" - extension: {fileInf.Extension}");
            }
            else
            {
                Console.WriteLine("Could't save changes in storage");
            }
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
