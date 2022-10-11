using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesSopAPI.Migrations
{
    public partial class add_nickname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NickName",
                table: "NhanVien",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NickName",
                table: "NhanVien");
        }
    }
}
