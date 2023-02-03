using CommandLine;
using FileStorage.Core.Services;
using FileStorage.Core.Services.Interfaces;
using FileStorage.UI.CLICommands.Interfaces;

namespace FileStorage.UI.CLICommands.CLIFileCommands
{
    [Verb("file_download", HelpText = "Downloading a file from the storage")]
    public class FileDownloadCommand : ICommand
    {
        private readonly IFileService fileService;
        public FileDownloadCommand(IFileService fileService)
        {
            this.fileService = fileService;
        }
        public FileDownloadCommand()
        {
            fileService = new FileService();
        }

        [Value(0, Required = true, HelpText = "Get file name")]
        public string FileName { get; set; }
        [Value(1, Required = true, HelpText = "Get directory path")]
        public string Directory { get; set; }

        public void Execute()
        {
            fileService.Download(FileName, Directory);
        }
    }
}
