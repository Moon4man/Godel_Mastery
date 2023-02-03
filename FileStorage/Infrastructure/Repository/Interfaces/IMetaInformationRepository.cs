using FileStorage.Infrastructure.Data.Models;
using System.Collections.Generic;

namespace FileStorage.Infrastructure.Repository.Interfaces
{
    public interface IMetaInformationRepository
    {
        bool Exists(string fileName);
        MetaInformation GetMetaFile(string fileName);
        List<MetaInformation> GetMetaInfoList();
        bool Remove(string fileName);
        bool Update(MetaInformation metaInformation, string fileName);
        bool Upload(MetaInformation metaInformation);
    }
}