using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModulesImplementation.Migrations
{
    /// <inheritdoc />
    public partial class Addedremindertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Reminders",
                newName: "ReminderDateTime");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Reminders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsSent",
                table: "Reminders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "IsSent",
                table: "Reminders");

            migrationBuilder.RenameColumn(
                name: "ReminderDateTime",
                table: "Reminders",
                newName: "DateTime");
        }
    }
}
