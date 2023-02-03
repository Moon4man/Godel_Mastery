using FileStorage.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileStorage.Infrastructure.Data
{
    public class DbSeeds : IEntityTypeConfiguration<MetaInformation>
    {
        public void Configure(EntityTypeBuilder<MetaInformation> builder)
        {
           builder.HasData(
               new MetaInformation
               {
                   Id = 1,
                   FileName = "HelloWorld.txt",
                   FileExtension = ".txt",
                   FileSize = 174,
                   FileCreationDate = "2022/04/08",
                   LastAccessTime = "2022/04/08",
                   DownloadNumber = 0
               },
               new MetaInformation
               {
                   Id=2,
                   FileName = "Universe.jpg",
                   FileExtension = ".jpg",
                   FileSize = 4585350,
                   FileCreationDate = "2022/04/08",
                   LastAccessTime = "2022/04/08",
                   DownloadNumber = 1
               },
               new MetaInformation
               {
                   Id = 3,
                   FileName = "The_little_prince.pdf",
                   FileExtension = ".pdf",
                   FileSize = 792631,
                   FileCreationDate = "2022/04/08",
                   LastAccessTime = "2022/04/08",
                   DownloadNumber = 3
               }
            );
        }
    }
}
