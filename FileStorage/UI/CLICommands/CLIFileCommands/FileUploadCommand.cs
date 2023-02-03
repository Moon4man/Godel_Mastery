using CommandLine;
using FileStorage.Core.Services;
using FileStorage.Core.Services.Interfaces;
using FileStorage.UI.CLICommands.Interfaces;

namespace FileStorage.UI.CLICommands.CLIFileCommands
{
    [Verb("file_upload", HelpText = "Uploading a file to the storage")]
    public class FileUploadCommand : ICommand
    {
        private readonly IFileService fileService;
        public FileUploadCommand(IFileService fileService)
        {
            this.fileService = fileService;
        }
        public FileUploadCommand()
        {
            fileService = new FileService();
        }

        [Value(0, Required = true, HelpText = "Get path to file")]
        public string FilePath { get; set; }

        public void Execute()
        {
            fileService.Upload(FilePath);
        }
    }
}
