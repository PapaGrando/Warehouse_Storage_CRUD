using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Storage.DataBase.Migrations
{
    public partial class fixed_models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllItems_Cells_CellId",
                table: "AllItems");

            migrationBuilder.DropForeignKey(
                name: "FK_AllItems_ItemsState_StateId",
                table: "AllItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Cells_Areas_AreaId",
                table: "Cells");

            migrationBuilder.DropForeignKey(
                name: "FK_Cells_CellTypes_CellTypeId",
                table: "Cells");

            migrationBuilder.DropTable(
                name: "ItemsState");

            migrationBuilder.DropIndex(
                name: "IX_Cells_AreaId",
                table: "Cells");

            migrationBuilder.DropIndex(
                name: "IX_Cells_CellTypeId",
                table: "Cells");

            migrationBuilder.DropIndex(
                name: "IX_AllItems_StateId",
                table: "AllItems");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Cells");

            migrationBuilder.DropColumn(
                name: "CellTypeId",
                table: "Cells");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "AllItems");

            migrationBuilder.RenameColumn(
                name: "NoOfSubArea",
                table: "SubAreas",
                newName: "CellTypeId");

            migrationBuilder.RenameColumn(
                name: "SubAreaWidthZ",
                table: "Cells",
                newName: "SubAreaWidthY");

            migrationBuilder.RenameColumn(
                name: "SubAreaLenghtX",
                table: "Cells",
                newName: "SubAreaLengthX");

            migrationBuilder.RenameColumn(
                name: "SubAreaHeightY",
                table: "Cells",
                newName: "SubAreaHeigthZ");

            migrationBuilder.AlterColumn<int>(
                name: "CellId",
                table: "AllItems",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddTime",
                table: "AllItems",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_SubAreas_CellTypeId",
                table: "SubAreas",
                column: "CellTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_Name",
                table: "ProductCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CellTypes_Name",
                table: "CellTypes",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AllItems_Cells_CellId",
                table: "AllItems",
                column: "CellId",
                principalTable: "Cells",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubAreas_CellTypes_CellTypeId",
                table: "SubAreas",
                column: "CellTypeId",
                principalTable: "CellTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllItems_Cells_CellId",
                table: "AllItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SubAreas_CellTypes_CellTypeId",
                table: "SubAreas");

            migrationBuilder.DropIndex(
                name: "IX_SubAreas_CellTypeId",
                table: "SubAreas");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_Name",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_CellTypes_Name",
                table: "CellTypes");

            migrationBuilder.DropColumn(
                name: "AddTime",
                table: "AllItems");

            migrationBuilder.RenameColumn(
                name: "CellTypeId",
                table: "SubAreas",
                newName: "NoOfSubArea");

            migrationBuilder.RenameColumn(
                name: "SubAreaWidthY",
                table: "Cells",
                newName: "SubAreaWidthZ");

            migrationBuilder.RenameColumn(
                name: "SubAreaLengthX",
                table: "Cells",
                newName: "SubAreaLenghtX");

            migrationBuilder.RenameColumn(
                name: "SubAreaHeigthZ",
                table: "Cells",
                newName: "SubAreaHeightY");

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Cells",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CellTypeId",
                table: "Cells",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CellId",
                table: "AllItems",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "AllItems",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ItemsState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsState", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cells_AreaId",
                table: "Cells",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cells_CellTypeId",
                table: "Cells",
                column: "CellTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AllItems_StateId",
                table: "AllItems",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_AllItems_Cells_CellId",
                table: "AllItems",
                column: "CellId",
                principalTable: "Cells",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllItems_ItemsState_StateId",
                table: "AllItems",
                column: "StateId",
                principalTable: "ItemsState",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cells_Areas_AreaId",
                table: "Cells",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cells_CellTypes_CellTypeId",
                table: "Cells",
                column: "CellTypeId",
                principalTable: "CellTypes",
                principalColumn: "Id");
        }
    }
}
