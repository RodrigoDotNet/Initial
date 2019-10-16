using Microsoft.EntityFrameworkCore.Migrations;

namespace Initial.Api.Migrations
{
    public partial class V1_0_20191013_1902 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Policy",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Policy_AreaId",
                table: "Policy",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policy_Area_AreaId",
                table: "Policy",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policy_Area_AreaId",
                table: "Policy");

            migrationBuilder.DropIndex(
                name: "IX_Policy_AreaId",
                table: "Policy");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Policy");
        }
    }
}
