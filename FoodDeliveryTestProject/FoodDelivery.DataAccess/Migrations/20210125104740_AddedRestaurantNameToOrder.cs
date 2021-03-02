using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodDelivery.DataAccess.Migrations
{
    public partial class AddedRestaurantNameToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RestaurantName",
                table: "OrdersSet",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RestaurantName",
                table: "OrdersSet");
        }
    }
}
