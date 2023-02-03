using FileStorage.Infrastructure.Data;
using FileStorage.Infrastructure.Data.Models;
using FileStorage.Infrastructure.Repository.Interfaces;

namespace FileStorage.Infrastructure.Repository
{
    public class MetaInformationRepository : IMetaInformationRepository
    {
        private AppDbContext context = new AppDbContext(); 

        public bool Exists(string fileName)
        {
            return context.MetaInformation.Any(m => m.FileName == fileName);
        }

        public MetaInformation GetMetaFile(string fileName)
        {
            return context.MetaInformation.FirstOrDefault(m => m.FileName == fileName);
        }

        public List<MetaInformation> GetMetaInfoList()
        {
            return context.MetaInformation.ToList();
        }

        public bool Remove(string fileName)
        {
            var metadata = GetMetaFile(fileName);
            if (metadata != null)
            {
                context.MetaInformation.Remove(metadata);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(MetaInformation metaInformation, string fileName)
        {
            var metadata = GetMetaFile(fileName);
            if (metadata != null)
            {
                context.Update(metadata);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Upload(MetaInformation metaInformation)
        {
            context.MetaInformation.Add(metaInformation);
            context.SaveChanges();
            return true;
        }
    }
}
