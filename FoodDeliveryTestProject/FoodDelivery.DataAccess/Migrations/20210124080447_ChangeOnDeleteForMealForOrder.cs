using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodDelivery.DataAccess.Migrations
{
    public partial class ChangeOnDeleteForMealForOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealsForOrdersSet_OrdersSet_OrderId",
                table: "MealsForOrdersSet");

            migrationBuilder.AddForeignKey(
                name: "FK_MealsForOrdersSet_OrdersSet_OrderId",
                table: "MealsForOrdersSet",
                column: "OrderId",
                principalTable: "OrdersSet",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealsForOrdersSet_OrdersSet_OrderId",
                table: "MealsForOrdersSet");

            migrationBuilder.AddForeignKey(
                name: "FK_MealsForOrdersSet_OrdersSet_OrderId",
                table: "MealsForOrdersSet",
                column: "OrderId",
                principalTable: "OrdersSet",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
