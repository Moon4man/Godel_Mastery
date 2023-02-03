using CommandLine;
using FileStorage.Core.Services;
using FileStorage.Core.Services.Interfaces;
using FileStorage.UI.CLICommands.Interfaces;
using System;

namespace FileSorage.UI.CLICommands.CLIExportCommand
{
    [Verb("file_export", HelpText = "Exporting metadata in the json or xml formats")]
    public class ExportMetaDataCommand : ICommand
    {
        private readonly IFormattersService formattersService;
        public ExportMetaDataCommand(IFormattersService formattersService)
        {
            this.formattersService = formattersService;
        }
        public ExportMetaDataCommand()
        {
            formattersService = new FormattersService();
        }

        [Value(0, HelpText = "Write the path to file")]
        public string FilePath { get; set; }
        [Option("format", HelpText = "Write the format")]
        public string Format { get; set; }
        [Option("info", HelpText = "Get all formats")]
        public bool Info { get; set; }

        public void Execute()
        {
            if (Info)
            {
                Console.WriteLine("\n - json" +
                                  "\n - xml");
            }
            else if (Format == "json")
            {
                formattersService.GetJsonFormat(FilePath);
            }
            else if (Format == "xml")
            {
                formattersService.GetXmlFormat(FilePath);
            }
            else
            {
                formattersService.GetJsonFormat(FilePath);
            }
        }
    }
}
