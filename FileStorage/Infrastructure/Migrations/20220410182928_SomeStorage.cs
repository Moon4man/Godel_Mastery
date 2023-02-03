using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class SomeStorage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MetaInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    FileCreationDate = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    LastAccessTime = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DownloadNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaInformation", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MetaInformation",
                columns: new[] { "Id", "DownloadNumber", "FileCreationDate", "FileExtension", "FileName", "FileSize", "LastAccessTime" },
                values: new object[] { 1, 0, "2022/04/08", ".txt", "HelloWorld.txt", 174L, "2022/04/08" });

            migrationBuilder.InsertData(
                table: "MetaInformation",
                columns: new[] { "Id", "DownloadNumber", "FileCreationDate", "FileExtension", "FileName", "FileSize", "LastAccessTime" },
                values: new object[] { 2, 1, "2022/04/08", ".jpg", "Universe.jpg", 4585350L, "2022/04/08" });

            migrationBuilder.InsertData(
                table: "MetaInformation",
                columns: new[] { "Id", "DownloadNumber", "FileCreationDate", "FileExtension", "FileName", "FileSize", "LastAccessTime" },
                values: new object[] { 3, 3, "2022/04/08", ".pdf", "The_little_prince.pdf", 792631L, "2022/04/08" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MetaInformation");
        }
    }
}
