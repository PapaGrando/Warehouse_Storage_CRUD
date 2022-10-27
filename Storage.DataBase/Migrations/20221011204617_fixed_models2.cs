using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Storage.DataBase.Migrations
{
    public partial class fixed_models2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllItems_Cells_CellId",
                table: "AllItems");

            migrationBuilder.AlterColumn<int>(
                name: "CellId",
                table: "AllItems",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_AllItems_Cells_CellId",
                table: "AllItems",
                column: "CellId",
                principalTable: "Cells",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllItems_Cells_CellId",
                table: "AllItems");

            migrationBuilder.AlterColumn<int>(
                name: "CellId",
                table: "AllItems",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AllItems_Cells_CellId",
                table: "AllItems",
                column: "CellId",
                principalTable: "Cells",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
