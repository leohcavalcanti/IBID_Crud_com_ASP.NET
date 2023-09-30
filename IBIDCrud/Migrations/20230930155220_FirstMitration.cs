using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IBIDCrud.Migrations
{
    /// <inheritdoc />
    public partial class FirstMitration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IBIDEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Start_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IBIDEvent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IBIDEventSpeakers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TalkTitle = table.Column<string>(type: "text", nullable: false),
                    TalkDescription = table.Column<string>(type: "text", nullable: false),
                    LinkedInProfile = table.Column<string>(type: "text", nullable: false),
                    IBIDEventId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IBIDEventSpeakers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IBIDEventSpeakers_IBIDEvent_IBIDEventId",
                        column: x => x.IBIDEventId,
                        principalTable: "IBIDEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IBIDEventSpeakers_IBIDEventId",
                table: "IBIDEventSpeakers",
                column: "IBIDEventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IBIDEventSpeakers");

            migrationBuilder.DropTable(
                name: "IBIDEvent");
        }
    }
}
