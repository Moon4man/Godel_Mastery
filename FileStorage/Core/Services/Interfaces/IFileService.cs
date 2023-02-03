namespace FileStorage.Core.Services.Interfaces
{
    public interface IFileService
    {
        void Download(string fileName, string path);
        void Find(string fileName);
        void GetInfo(string fileName);
        void Move(string oldName, string newName);
        void Remove(string fileName);
        void Upload(string path);
    }
}
