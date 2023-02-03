using CommandLine;
using System.Text;
using FileStorage.UI.CLICommands.Interfaces;
using FileStorage.UI.CLICommands.CLIFileCommands;
using FileStorage.UI.CLICommands.CLIUserCommands;
using FileSorage.UI.CLICommands.CLIExportCommand;

namespace FileStorage.UI
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Parser.Default.ParseArguments<UserInfoCommand,
                FileInfoCommand,
                FileUploadCommand,
                FileDownloadCommand,
                FileMoveCommand,
                FileRemoveCommand,
                FileFindCommand,
                AuthCommand,
                ExportMetaDataCommand>(args)
                .WithParsed<ICommand>(p => p.Execute());
        }
    }
}
