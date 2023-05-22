using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expedia.API.Migrations
{
    /// <inheritdoc />
    public partial class orderMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    CreateDateUTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionMetadata = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fb6d4f10-79ed-4aff-a915-4ce29dc9c7e0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "975ae57d-f078-481c-b67e-ceacd446810e", "AQAAAAIAAYagAAAAEFEsS/bvhnF0LkTvq8LRqwjhbT8yDio5ikO+s+J/z2I6hlbwd9gUPNiQMvaSSgB8jw==", "891d2705-5392-482c-af32-15e6c62eeefb" });

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_OrderId",
                table: "LineItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LineItems_Orders_OrderId",
                table: "LineItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineItems_Orders_OrderId",
                table: "LineItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_LineItems_OrderId",
                table: "LineItems");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fb6d4f10-79ed-4aff-a915-4ce29dc9c7e0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1fe2a1a4-043a-41a8-8cbd-d4da5c1f56a2", "AQAAAAIAAYagAAAAEOVzTLFYdMYI3uOVdMEf5BGDo+nFpPwEdcDpExWRfrlPNCbGGiIdUDPb5Hco1h64dg==", "2948e33c-0316-4646-a630-073cd12d8ec6" });
        }
    }
}
