using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolFinder.Core.Migrations
{
    public partial class SchoolCreationModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "SchoolCreationRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "SchoolCreationRequests");
        }
    }
}
