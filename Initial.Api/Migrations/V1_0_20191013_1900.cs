using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Initial.Api.Migrations
{
    public partial class V1_0_20191013_1900 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enterprise",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreationUserId = table.Column<int>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedUserId = table.Column<int>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    PrivateId = table.Column<Guid>(nullable: false),
                    PublicId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enterprise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(nullable: true),
                    MessageTemplate = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Level = table.Column<string>(maxLength: 128, nullable: true),
                    Exception = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreationUserId = table.Column<int>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedUserId = table.Column<int>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    PrivateId = table.Column<Guid>(nullable: false),
                    PublicId = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(maxLength: 250, nullable: false),
                    Password = table.Column<Guid>(nullable: false),
                    EnterpriseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Enterprise_EnterpriseId",
                        column: x => x.EnterpriseId,
                        principalTable: "Enterprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_EnterpriseId",
                table: "User",
                column: "EnterpriseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Enterprise");
        }
    }
}
