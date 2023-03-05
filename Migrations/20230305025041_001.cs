using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace realEstateWebApp.Migrations
{
    /// <inheritdoc />
    public partial class _001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageModel_Biens_BienModelId",
                table: "ImageModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageModel",
                table: "ImageModel");

            migrationBuilder.RenameTable(
                name: "ImageModel",
                newName: "ImagesBiens");

            migrationBuilder.RenameIndex(
                name: "IX_ImageModel_BienModelId",
                table: "ImagesBiens",
                newName: "IX_ImagesBiens_BienModelId");

            migrationBuilder.AlterColumn<string>(
                name: "ImageDeBienUrl",
                table: "Biens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SIN",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImagesBiens",
                table: "ImagesBiens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesBiens_Biens_BienModelId",
                table: "ImagesBiens",
                column: "BienModelId",
                principalTable: "Biens",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagesBiens_Biens_BienModelId",
                table: "ImagesBiens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImagesBiens",
                table: "ImagesBiens");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SIN",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "ImagesBiens",
                newName: "ImageModel");

            migrationBuilder.RenameIndex(
                name: "IX_ImagesBiens_BienModelId",
                table: "ImageModel",
                newName: "IX_ImageModel_BienModelId");

            migrationBuilder.AlterColumn<string>(
                name: "ImageDeBienUrl",
                table: "Biens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageModel",
                table: "ImageModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageModel_Biens_BienModelId",
                table: "ImageModel",
                column: "BienModelId",
                principalTable: "Biens",
                principalColumn: "Id");
        }
    }
}
