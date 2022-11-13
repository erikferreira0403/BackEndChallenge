using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioFinal.Migrations
{
    public partial class RemoveStatusEnumSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusEnum",
                table: "Subscriptions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StatusEnum",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
