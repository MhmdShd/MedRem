using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedRem.Migrations
{
    /// <inheritdoc />
    public partial class converttimetotimeonly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "PillSchedules",
                newName: "TimeAsString");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeAsString",
                table: "PillSchedules",
                newName: "Time");
        }
    }
}
