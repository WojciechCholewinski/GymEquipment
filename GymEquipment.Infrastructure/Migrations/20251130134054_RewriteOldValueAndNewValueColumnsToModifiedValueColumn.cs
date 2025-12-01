using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymEquipment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RewriteOldValueAndNewValueColumnsToModifiedValueColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewValue",
                table: "ProductHistory");

            migrationBuilder.RenameColumn(
                name: "OldValue",
                table: "ProductHistory",
                newName: "ModifiedValue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedValue",
                table: "ProductHistory",
                newName: "OldValue");

            migrationBuilder.AddColumn<string>(
                name: "NewValue",
                table: "ProductHistory",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);
        }
    }
}
