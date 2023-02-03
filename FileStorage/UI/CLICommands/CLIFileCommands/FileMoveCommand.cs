using CommandLine;
using FileStorage.Core.Services;
using FileStorage.Core.Services.Interfaces;
using FileStorage.UI.CLICommands.Interfaces;

namespace FileStorage.UI.CLICommands.CLIFileCommands
{
    [Verb("file_move", HelpText = "Renaming file in the storage")]
    public class FileMoveCommand : ICommand
    {
        private readonly IFileService fileService;
        public FileMoveCommand(IFileService fileService)
        {
            this.fileService = fileService;
        }
        public FileMoveCommand()
        {
            fileService = new FileService();
        }

        [Value(0, Required = true, HelpText = "Write the name of file")]
        public string OldName { get; set; }
        [Value(1, Required = true, HelpText = "Write the new name of file")]
        public string NewName { get; set; }

        public void Execute()
        {
            fileService.Move(OldName, NewName);
        }
    }
}
