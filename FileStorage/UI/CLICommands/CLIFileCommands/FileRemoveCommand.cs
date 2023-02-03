using CommandLine;
using FileStorage.Core.Services;
using FileStorage.Core.Services.Interfaces;
using FileStorage.UI.CLICommands.Interfaces;

namespace FileStorage.UI.CLICommands.CLIFileCommands
{
    [Verb("file_remove", HelpText = "Removing a file from the storage")]
    public class FileRemoveCommand : ICommand
    {
        private readonly IFileService fileService;
        public FileRemoveCommand(IFileService fileService)
        {
            this.fileService = fileService;
        }
        public FileRemoveCommand()
        {
            fileService = new FileService();
        }

        [Value(0, Required = true, HelpText = "Write the name of file")]
        public string FileName { get; set; }

        public void Execute()
        {
            fileService.Remove(FileName);
        }
    }
}
