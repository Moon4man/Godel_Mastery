using System;

namespace FileStorage.Infrastructure.Data.Models
{
    [Serializable]
    public class MetaInformation
    {
        public int Id { get; set; }    
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public long FileSize { get; set; }
        public string FileCreationDate { get; set; }
        public string LastAccessTime { get; set; }
        public int DownloadNumber { get; set; }
    }
}
