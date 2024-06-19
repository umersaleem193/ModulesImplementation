using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModulesImplementation.Migrations
{
    /// <inheritdoc />
    public partial class Addedremindertables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reminders",
                table: "Reminders");

            migrationBuilder.RenameTable(
                name: "Reminders",
                newName: "Reminder");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reminder",
                table: "Reminder",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reminder",
                table: "Reminder");

            migrationBuilder.RenameTable(
                name: "Reminder",
                newName: "Reminders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reminders",
                table: "Reminders",
                column: "Id");
        }
    }
}
