using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Result",
                table: "Barns",
                newName: "Note");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Records",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Records");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Barns",
                newName: "Result");
        }
    }
}
