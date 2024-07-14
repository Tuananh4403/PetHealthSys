using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class addTotal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Staff",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Records",
                type: "decimal(18,2)",
                nullable: true,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Doctors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Shift",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Doctors");

            migrationBuilder.AlterColumn<int>(
                name: "Shift",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
