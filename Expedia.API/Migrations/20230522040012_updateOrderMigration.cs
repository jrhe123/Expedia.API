using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expedia.API.Migrations
{
    /// <inheritdoc />
    public partial class updateOrderMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TransactionMetadata",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fb6d4f10-79ed-4aff-a915-4ce29dc9c7e0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a7de4f9a-155d-40a6-b438-a3f7c31a7889", "AQAAAAIAAYagAAAAEN/jcCUntmh5wxwsKIsB66MFBHCuSh1Z6VnRow6/+MJkPkai0vvXcX4DKYBO4Y+FyQ==", "2c31444e-136a-4f9f-b018-c7df697be8c4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TransactionMetadata",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fb6d4f10-79ed-4aff-a915-4ce29dc9c7e0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "975ae57d-f078-481c-b67e-ceacd446810e", "AQAAAAIAAYagAAAAEFEsS/bvhnF0LkTvq8LRqwjhbT8yDio5ikO+s+J/z2I6hlbwd9gUPNiQMvaSSgB8jw==", "891d2705-5392-482c-af32-15e6c62eeefb" });
        }
    }
}
