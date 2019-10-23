using Microsoft.EntityFrameworkCore.Migrations;

namespace Initial.Api.Migrations
{
    public partial class V1_0_20191020_2001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnterpriseId",
                table: "Group",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Group_EnterpriseId",
                table: "Group",
                column: "EnterpriseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Enterprise_EnterpriseId",
                table: "Group",
                column: "EnterpriseId",
                principalTable: "Enterprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Enterprise_EnterpriseId",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Group_EnterpriseId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "EnterpriseId",
                table: "Group");
        }
    }
}
