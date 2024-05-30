using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManageRecord_Records_RecordId",
                table: "ManageRecord");

            migrationBuilder.AddForeignKey(
                name: "FK_ManageRecord_Records_RecordId",
                table: "ManageRecord",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "RecordId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManageRecord_Records_RecordId",
                table: "ManageRecord");

            migrationBuilder.AddForeignKey(
                name: "FK_ManageRecord_Records_RecordId",
                table: "ManageRecord",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "RecordId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
