using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Storage.DataBase.Migrations
{
    public partial class changedvalidation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CellTypes_Cells_CellId",
                table: "CellTypes");

            migrationBuilder.DropIndex(
                name: "IX_CellTypes_CellId",
                table: "CellTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SubAreas");

            migrationBuilder.DropColumn(
                name: "CellId",
                table: "CellTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cells");

            migrationBuilder.AddColumn<int>(
                name: "NoOfSubArea",
                table: "SubAreas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ItemsState",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Areas",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoOfSubArea",
                table: "SubAreas");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SubAreas",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ItemsState",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "CellId",
                table: "CellTypes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cells",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Areas",
                type: "character varying(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15);

            migrationBuilder.CreateIndex(
                name: "IX_CellTypes_CellId",
                table: "CellTypes",
                column: "CellId");

            migrationBuilder.AddForeignKey(
                name: "FK_CellTypes_Cells_CellId",
                table: "CellTypes",
                column: "CellId",
                principalTable: "Cells",
                principalColumn: "Id");
        }
    }
}
