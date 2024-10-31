using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Walks.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedPropertiesforeachModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Walks",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DifficultyId",
                table: "Walks",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "Length",
                table: "Walks",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Walks",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "RegionId",
                table: "Walks",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "WalkImageUrl",
                table: "Walks",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Regions",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Regions",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "RegionImageUrl",
                table: "Regions",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Difficulty",
                type: "longtext",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Walks_DifficultyId",
                table: "Walks",
                column: "DifficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Walks_RegionId",
                table: "Walks",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_Difficulty_DifficultyId",
                table: "Walks",
                column: "DifficultyId",
                principalTable: "Difficulty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_Regions_RegionId",
                table: "Walks",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_Difficulty_DifficultyId",
                table: "Walks");

            migrationBuilder.DropForeignKey(
                name: "FK_Walks_Regions_RegionId",
                table: "Walks");

            migrationBuilder.DropIndex(
                name: "IX_Walks_DifficultyId",
                table: "Walks");

            migrationBuilder.DropIndex(
                name: "IX_Walks_RegionId",
                table: "Walks");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Walks");

            migrationBuilder.DropColumn(
                name: "DifficultyId",
                table: "Walks");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Walks");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Walks");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "Walks");

            migrationBuilder.DropColumn(
                name: "WalkImageUrl",
                table: "Walks");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "RegionImageUrl",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Difficulty");
        }
    }
}
