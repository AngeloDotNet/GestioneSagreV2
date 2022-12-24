using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestioneSagre.Utility.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmailId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Recipient = table.Column<string>(type: "TEXT", nullable: true),
                    RecipientEmail = table.Column<string>(type: "TEXT", nullable: true),
                    Subject = table.Column<string>(type: "TEXT", nullable: true),
                    Message = table.Column<string>(type: "TEXT", nullable: true),
                    SendDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EffectiveSendDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EmailSendCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailMessages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailMessages");
        }
    }
}
