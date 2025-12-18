using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrivingSchool.Migrations
{
    /// <inheritdoc />
    public partial class updatingApointmentsForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarType",
                table: "Appointment",
                newName: "PostalCode");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Appointment");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "Appointment",
                newName: "CarType");
        }
    }
}
