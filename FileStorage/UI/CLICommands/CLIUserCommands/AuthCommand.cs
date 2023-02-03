using CommandLine;
using FileStorage.Core.Services;
using FileStorage.Core.Services.Interfaces;
using FileStorage.UI.CLICommands.Interfaces;
using System;

namespace FileStorage.UI.CLICommands.CLIUserCommands
{
    [Verb("auth", HelpText = "Authorization in app")]
    public class AuthCommand : ICommand
    {
        private readonly IAccountService accountService;

        public AuthCommand(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        public AuthCommand()
        {
            accountService = new AccountService();
        }

        [Option("login", Required = true, HelpText = "Write the login")]
        public string Login { get; set; }
        [Option("password", Required = true, HelpText = "Write the password")]
        public string Password { get; set; }

        public void Execute()
        {
            if (accountService.GetAuthenticated(Login, Password))
            {
                Console.WriteLine("\nAuthorization was successful!");
            }
            else
            {
                Console.WriteLine("\nIncorrect login and/or password. Please, try again!");
            }
        }
    }
}
