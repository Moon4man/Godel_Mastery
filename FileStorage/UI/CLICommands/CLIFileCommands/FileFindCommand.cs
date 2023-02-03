using CommandLine;
using FileStorage.Core.Services;
using FileStorage.Core.Services.Interfaces;
using FileStorage.UI.CLICommands.Interfaces;

namespace FileStorage.UI.CLICommands.CLIFileCommands
{
    [Verb("file_find", HelpText = "Searching file in storage")]
    public class FileFindCommand : ICommand
    {
        private readonly IFileService fileService;
        public FileFindCommand(IFileService fileService)
        {
            this.fileService = fileService;
        }
        public FileFindCommand()
        {
            fileService = new FileService();
        }

        [Value(0, Required = true, HelpText = "Write the name of file")]
        public string FileName { get; set; }

        public void Execute()
        {
            fileService.Find(FileName);
        }
    }
}
