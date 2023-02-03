namespace FileStorage.Core.Services.Interfaces
{
    public interface IFormattersService
    {
        void GetJsonFormat(string path);
        void GetXmlFormat(string path);
    }
}