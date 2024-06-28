using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class addUnitService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Unit",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Services");
        }
    }
}
