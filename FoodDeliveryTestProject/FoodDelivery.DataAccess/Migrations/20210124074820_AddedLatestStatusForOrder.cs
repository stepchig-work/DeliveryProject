﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodDelivery.DataAccess.Migrations
{
    public partial class AddedLatestStatusForOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LatestOrderStatus",
                table: "OrdersSet",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LatestOrderStatus",
                table: "OrdersSet");
        }
    }
}
