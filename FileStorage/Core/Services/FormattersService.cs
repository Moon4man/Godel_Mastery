using FileStorage.Core.Services.Interfaces;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using FileStorage.Infrastructure.Repository.Interfaces;
using FileStorage.Infrastructure.Data.Models;
using FileStorage.Infrastructure.Repository;

namespace FileStorage.Core.Services
{
    public class FormattersService : IFormattersService
    {
        private readonly IMetaInformationRepository metaInformationRepository;

        public FormattersService(IMetaInformationRepository metaInformationRepository, IUserService userService)
        {
            this.metaInformationRepository = metaInformationRepository;
        }
        public FormattersService()
        {
            metaInformationRepository = new MetaInformationRepository();
        }

        public void GetJsonFormat(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
                Console.WriteLine("File created! But export didn't happen, try again");
                return;
            }

            List<MetaInformation> metadataList = metaInformationRepository.GetMetaInfoList();

            try
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(metadataList));
                Console.WriteLine($"\nThe meta-information has been exported, path = '{path}' ");
            }
            catch (SerializationException ex)
            {
                Console.WriteLine("\nFailed to formatted.\n Reason: " + ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine("\nFailed to formatted.\n Reason: " + ex.Message);
            }

        }

        public void GetXmlFormat(string path)
        {
            List<MetaInformation> metadataList = metaInformationRepository.GetMetaInfoList();

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                try
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(List<MetaInformation>));
                    formatter.Serialize(fs, metadataList);
                    Console.WriteLine($"\nThe meta-information has been exported, path = '{path}' ");
                }
                catch (SerializationException ex)
                {
                    Console.WriteLine("\nFailed to formatted.\n Reason: " + ex.Message);
                }
            }
        }
    }
}
