using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodDelivery.DataAccess.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountsSet",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsSet", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "BlockedUsersSet",
                columns: table => new
                {
                    BlockedUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockedUsersSet", x => x.BlockedUserId);
                    table.ForeignKey(
                        name: "FK_BlockedUsersSet_AccountsSet_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AccountsSet",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdersSet",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(14,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersSet", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrdersSet_AccountsSet_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AccountsSet",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantsSet",
                columns: table => new
                {
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantsSet", x => x.RestaurantId);
                    table.ForeignKey(
                        name: "FK_RestaurantsSet_AccountsSet_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AccountsSet",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealsForOrdersSet",
                columns: table => new
                {
                    MealForOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmmountOfMeals = table.Column<int>(type: "int", nullable: false),
                    MealName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MealPrice = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealsForOrdersSet", x => x.MealForOrderId);
                    table.ForeignKey(
                        name: "FK_MealsForOrdersSet_OrdersSet_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrdersSet",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatusesSet",
                columns: table => new
                {
                    OrderStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusChangeTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatusesSet", x => x.OrderStatusId);
                    table.ForeignKey(
                        name: "FK_OrderStatusesSet_OrdersSet_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrdersSet",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealsSet",
                columns: table => new
                {
                    MealId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealsSet", x => x.MealId);
                    table.ForeignKey(
                        name: "FK_MealsSet_RestaurantsSet_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "RestaurantsSet",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantsImagesSet",
                columns: table => new
                {
                    RestaurantImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(type: "image", nullable: true),
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantsImagesSet", x => x.RestaurantImageId);
                    table.ForeignKey(
                        name: "FK_RestaurantsImagesSet_RestaurantsSet_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "RestaurantsSet",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealsImagesSet",
                columns: table => new
                {
                    MealImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(type: "image", nullable: true),
                    MealId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealsImagesSet", x => x.MealImageId);
                    table.ForeignKey(
                        name: "FK_MealsImagesSet_MealsSet_MealId",
                        column: x => x.MealId,
                        principalTable: "MealsSet",
                        principalColumn: "MealId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockedUsersSet_OwnerId",
                table: "BlockedUsersSet",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_MealsForOrdersSet_OrderId",
                table: "MealsForOrdersSet",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_MealsImagesSet_MealId",
                table: "MealsImagesSet",
                column: "MealId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MealsSet_RestaurantId",
                table: "MealsSet",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersSet_CustomerId",
                table: "OrdersSet",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusesSet_OrderId",
                table: "OrderStatusesSet",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantsImagesSet_RestaurantId",
                table: "RestaurantsImagesSet",
                column: "RestaurantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantsSet_OwnerId",
                table: "RestaurantsSet",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockedUsersSet");

            migrationBuilder.DropTable(
                name: "MealsForOrdersSet");

            migrationBuilder.DropTable(
                name: "MealsImagesSet");

            migrationBuilder.DropTable(
                name: "OrderStatusesSet");

            migrationBuilder.DropTable(
                name: "RestaurantsImagesSet");

            migrationBuilder.DropTable(
                name: "MealsSet");

            migrationBuilder.DropTable(
                name: "OrdersSet");

            migrationBuilder.DropTable(
                name: "RestaurantsSet");

            migrationBuilder.DropTable(
                name: "AccountsSet");
        }
    }
}
