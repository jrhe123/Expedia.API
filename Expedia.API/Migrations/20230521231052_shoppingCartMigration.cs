using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expedia.API.Migrations
{
    /// <inheritdoc />
    public partial class shoppingCartMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPercent = table.Column<double>(type: "float", nullable: true),
                    TouristRouteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShoppingCartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineItems_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LineItems_TouristRoutes_TouristRouteId",
                        column: x => x.TouristRouteId,
                        principalTable: "TouristRoutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fb6d4f10-79ed-4aff-a915-4ce29dc9c7e0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1fe2a1a4-043a-41a8-8cbd-d4da5c1f56a2", "AQAAAAIAAYagAAAAEOVzTLFYdMYI3uOVdMEf5BGDo+nFpPwEdcDpExWRfrlPNCbGGiIdUDPb5Hco1h64dg==", "2948e33c-0316-4646-a630-073cd12d8ec6" });

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_ShoppingCartId",
                table: "LineItems",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_TouristRouteId",
                table: "LineItems",
                column: "TouristRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineItems");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fb6d4f10-79ed-4aff-a915-4ce29dc9c7e0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "90c2ed31-b48c-41a1-88ba-d9869371ecf6", "AQAAAAIAAYagAAAAEONewAl2AJFaxJloaVkhE1QG4/FSg5BeI5PctZqILw8gloM0x/fBqEuab9AshXS+dg==", "19ff2c36-e598-4daf-895e-6258b0313914" });
        }
    }
}
