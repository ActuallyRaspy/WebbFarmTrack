using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmTrack.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFarmContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_PlantedCrop_PlantedCropId",
                table: "Alerts");

            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Users_UserId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantedCrop_Crop_CropId",
                table: "PlantedCrop");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantedCrop_Fields_FieldId",
                table: "PlantedCrop");

            migrationBuilder.DropIndex(
                name: "IX_Fields_UserId",
                table: "Fields");

            migrationBuilder.DropIndex(
                name: "IX_Alerts_PlantedCropId",
                table: "Alerts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlantedCrop",
                table: "PlantedCrop");

            migrationBuilder.DropIndex(
                name: "IX_PlantedCrop_FieldId",
                table: "PlantedCrop");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ExpectedHarvestDate",
                table: "PlantedCrop");

            migrationBuilder.RenameTable(
                name: "PlantedCrop",
                newName: "PlantedCrops");

            migrationBuilder.RenameIndex(
                name: "IX_PlantedCrop_CropId",
                table: "PlantedCrops",
                newName: "IX_PlantedCrops_CropId");

            migrationBuilder.AlterColumn<int>(
                name: "FieldId",
                table: "Fields",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "AlertId",
                table: "Alerts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "PlantedCropId",
                table: "PlantedCrops",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlantedCrops",
                table: "PlantedCrops",
                column: "PlantedCropId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_PlantedCrops_AlertId",
                table: "Alerts",
                column: "AlertId",
                principalTable: "PlantedCrops",
                principalColumn: "PlantedCropId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Users_FieldId",
                table: "Fields",
                column: "FieldId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantedCrops_Crop_CropId",
                table: "PlantedCrops",
                column: "CropId",
                principalTable: "Crop",
                principalColumn: "CropId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantedCrops_Fields_PlantedCropId",
                table: "PlantedCrops",
                column: "PlantedCropId",
                principalTable: "Fields",
                principalColumn: "FieldId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_PlantedCrops_AlertId",
                table: "Alerts");

            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Users_FieldId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantedCrops_Crop_CropId",
                table: "PlantedCrops");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantedCrops_Fields_PlantedCropId",
                table: "PlantedCrops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlantedCrops",
                table: "PlantedCrops");

            migrationBuilder.RenameTable(
                name: "PlantedCrops",
                newName: "PlantedCrop");

            migrationBuilder.RenameIndex(
                name: "IX_PlantedCrops_CropId",
                table: "PlantedCrop",
                newName: "IX_PlantedCrop_CropId");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "FieldId",
                table: "Fields",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "AlertId",
                table: "Alerts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "PlantedCropId",
                table: "PlantedCrop",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpectedHarvestDate",
                table: "PlantedCrop",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlantedCrop",
                table: "PlantedCrop",
                column: "PlantedCropId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_UserId",
                table: "Fields",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_PlantedCropId",
                table: "Alerts",
                column: "PlantedCropId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantedCrop_FieldId",
                table: "PlantedCrop",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_PlantedCrop_PlantedCropId",
                table: "Alerts",
                column: "PlantedCropId",
                principalTable: "PlantedCrop",
                principalColumn: "PlantedCropId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Users_UserId",
                table: "Fields",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantedCrop_Crop_CropId",
                table: "PlantedCrop",
                column: "CropId",
                principalTable: "Crop",
                principalColumn: "CropId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantedCrop_Fields_FieldId",
                table: "PlantedCrop",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "FieldId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
