using Microsoft.EntityFrameworkCore.Migrations;

namespace Vchat.Data.Migrations
{
    public partial class mount_8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cryptmessage",
                table: "CryptMessages",
                newName: "Cryptmessage");

            migrationBuilder.AddColumn<string>(
                name: "ToSendId",
                table: "CryptMessages",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToSendId",
                table: "CryptMessages");

            migrationBuilder.RenameColumn(
                name: "Cryptmessage",
                table: "CryptMessages",
                newName: "cryptmessage");
        }
    }
}
