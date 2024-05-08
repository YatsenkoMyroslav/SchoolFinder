using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolFinder.Core.Migrations
{
    public partial class SchoolRegistrationAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SchoolCreationRequestId",
                table: "FileBytes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SchoolCreationRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LongDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolWebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolCreationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchoolCreationRequests_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SchoolCreationRequests_Geo_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Geo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileBytes_SchoolCreationRequestId",
                table: "FileBytes",
                column: "SchoolCreationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolCreationRequests_LocationId",
                table: "SchoolCreationRequests",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolCreationRequests_OwnerId",
                table: "SchoolCreationRequests",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileBytes_SchoolCreationRequests_SchoolCreationRequestId",
                table: "FileBytes",
                column: "SchoolCreationRequestId",
                principalTable: "SchoolCreationRequests",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileBytes_SchoolCreationRequests_SchoolCreationRequestId",
                table: "FileBytes");

            migrationBuilder.DropTable(
                name: "SchoolCreationRequests");

            migrationBuilder.DropIndex(
                name: "IX_FileBytes_SchoolCreationRequestId",
                table: "FileBytes");

            migrationBuilder.DropColumn(
                name: "SchoolCreationRequestId",
                table: "FileBytes");
        }
    }
}
