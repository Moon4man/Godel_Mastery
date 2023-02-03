using CommandLine;
using FileStorage.Core.Services;
using FileStorage.Core.Services.Interfaces;
using FileStorage.UI.CLICommands.Interfaces;

namespace FileStorage.UI.CLICommands.CLIUserCommands
{
    [Verb("user_info", HelpText = "Get information about user")]
    public class UserInfoCommand : ICommand
    {
        private readonly IUserService userService;
        public UserInfoCommand(IUserService userService)
        {
            this.userService = userService;
        }
        public UserInfoCommand()
        {
            userService = new UserService();
        }

        public void Execute()
        {
            userService.GetInfo();
        }
    }
}
