using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolFinder.Core.Migrations
{
    public partial class AddedCommentCreationRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentCreationRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentCreationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentCreationRequests_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentCreationRequests_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingCreationRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingCreationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingCreationRequests_CommentCreationRequests_CommentRequestId",
                        column: x => x.CommentRequestId,
                        principalTable: "CommentCreationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentCreationRequests_CreatedById",
                table: "CommentCreationRequests",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CommentCreationRequests_SchoolId",
                table: "CommentCreationRequests",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingCreationRequests_CommentRequestId",
                table: "RatingCreationRequests",
                column: "CommentRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RatingCreationRequests");

            migrationBuilder.DropTable(
                name: "CommentCreationRequests");
        }
    }
}
