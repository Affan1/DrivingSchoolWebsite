using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrivingSchool.Migrations
{
    /// <inheritdoc />
    public partial class Editing_Questions_With_PhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Questions");
        }
    }
}
