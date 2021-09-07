using Microsoft.EntityFrameworkCore.Migrations;

namespace PEM_SENIOR_PROJECT_BACK_END_TEST.Migrations
{
    public partial class addingmaincategorydescriptioncolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryDescription",
                table: "MainCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryDescription",
                table: "MainCategories");
        }
    }
}
