using Microsoft.EntityFrameworkCore.Migrations;

namespace Vchat.Data.Migrations
{
    public partial class mount_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConnectUser",
                table: "MyContacts",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectUser",
                table: "MyContacts");
        }
    }
}
