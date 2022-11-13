using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioFinal.Migrations
{
    public partial class InitiEventHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EventHistories_SubscriptionId",
                table: "EventHistories");

            migrationBuilder.AddColumn<string>(
                name: "StatusEnum",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventHistories_SubscriptionId",
                table: "EventHistories",
                column: "SubscriptionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EventHistories_SubscriptionId",
                table: "EventHistories");

            migrationBuilder.DropColumn(
                name: "StatusEnum",
                table: "Subscriptions");

            migrationBuilder.CreateIndex(
                name: "IX_EventHistories_SubscriptionId",
                table: "EventHistories",
                column: "SubscriptionId");
        }
    }
}
