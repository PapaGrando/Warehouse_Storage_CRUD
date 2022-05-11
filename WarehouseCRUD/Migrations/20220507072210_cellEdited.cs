using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseCRUD.Storage.Migrations
{
    public partial class cellEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cells",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cells");
        }
    }
}
