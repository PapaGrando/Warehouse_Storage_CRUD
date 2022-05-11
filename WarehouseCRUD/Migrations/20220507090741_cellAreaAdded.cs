using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseCRUD.Storage.Migrations
{
    public partial class cellAreaAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Cells",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cells_AreaId",
                table: "Cells",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cells_Areas_AreaId",
                table: "Cells",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cells_Areas_AreaId",
                table: "Cells");

            migrationBuilder.DropIndex(
                name: "IX_Cells_AreaId",
                table: "Cells");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Cells");
        }
    }
}
