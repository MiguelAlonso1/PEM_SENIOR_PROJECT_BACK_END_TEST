using Microsoft.EntityFrameworkCore.Migrations;

namespace PEM_SENIOR_PROJECT_BACK_END_TEST.Migrations
{
    public partial class AddingimagefieldtoMainKategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "MainCategories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "MainCategories");
        }
    }
}