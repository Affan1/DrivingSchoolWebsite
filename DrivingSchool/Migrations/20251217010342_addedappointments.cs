using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrivingSchool.Migrations
{
    /// <inheritdoc />
    public partial class addedappointments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Appointment",
                newName: "Datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Questions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Agreement",
                table: "Appointment",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Agreement",
                table: "Appointment");

            migrationBuilder.RenameColumn(
                name: "Datetime",
                table: "Appointment",
                newName: "DateTime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Questions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
