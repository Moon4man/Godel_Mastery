using CommandLine;
using FileStorage.Core.Services;
using FileStorage.Core.Services.Interfaces;
using FileStorage.UI.CLICommands.Interfaces;

namespace FileStorage.UI.CLICommands.CLIFileCommands
{
    [Verb("file_info", HelpText = "Get information about file")]
    public class FileInfoCommand : ICommand
    {
        private readonly IFileService fileService;
        public FileInfoCommand(IFileService fileService)
        {
            this.fileService = fileService;
        }
        public FileInfoCommand()
        {
            fileService = new FileService();
        }

        [Value(0, Required = true, HelpText = "Write the name of file")]
        public string FileName { get; set; }

        public void Execute()
        {
            fileService.GetInfo(FileName);
        }
    }
}
