﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "saveBarn",
                table: "Records",
                newName: "SaveBarn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SaveBarn",
                table: "Records",
                newName: "saveBarn");
        }
    }
}
