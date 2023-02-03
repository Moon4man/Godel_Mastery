
# ***The File Storage***

A simple console application for working with file storage.

### Framework / Library 
- .NET Core 6.0 *(Code)*
- Microsoft Sql Server *(DB)*
- Entity Framework Core *(ORM)*

### Steps to run

- Update the connection string in AppDbContext.cs in FileStorage.Infrastructure.Data
- Build the whole solution
- In Solution Explorer, make sure that FileStorage.UI is selected as the Startup Project
- Open the Package Manager Console Window and make sure that FileStorage.Infrastructure is selected as the Default project. Then type "Update-Database" then press "Enter"
- Open the CLI, go to "File Storage.UI" and enter one of the following commands

To log in to the application, you must enter in the command line:
dotnet run auth --login "login" --password "password"

--------------------------------
Login: SomeUser
Password: MyLittlePony45
--------------------------------

To work correctly, you need to change the path to the storage in app.config!

Max file size - 150 MB

### Commands:

- user_info 
(Get user information)
- file_upload <path-to-file> 
(Upload file to the storage)
- file_download <file-name> <destination-path> 
(Download file to the storage)
- file_move <source-file-name> <destination-file-name> 
(Rename file in the storage)
- file_remove <file-name> 
(Delete file from storage)
- file_info <file-name> 
(Get file information)
- file_find <file-name> 
(Find file in the storage)
- file_export <destination-path> --format <format> 
(Saving meta information about files in a specified format)
- file_export --info 
(Displaying a list of supported formats)

### The following NuGet packages are used in the application:

- System.Configuration.ConfigurationManager (To work with the configuration file)
- Newtonsoft.Json (To format a binary metafile in Json format)
- CommandLineParser (To work with CLI)
- Microsofy.EntityFrameworkCore (To work with DB)
- Microsofy.EntityFrameworkCore.Design
- Microsofy.EntityFrameworkCore.Tools
- Microsofy.EntityFrameworkCore.SqlServer