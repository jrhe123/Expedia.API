using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Expedia.API.Migrations
{
    /// <inheritdoc />
    public partial class dataSeeding2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("0642a314-95f3-4093-9d81-217078b4eeaf"));

            migrationBuilder.InsertData(
                table: "TouristRoutes",
                columns: new[] { "Id", "CreateTime", "DepartureTime", "Description", "DiscountPercent", "Features", "Fees", "Notes", "OriginalPrice", "Title", "UpdateTime" },
                values: new object[,]
                {
                    { new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "this is description 1", 0.01, "<div class\"bd\"><p style=\"text-align:center\"><span>features </span><strong></strong></p></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>fee includes: 1</dt><dd><div>123</div></dd></dl></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>notes includes: 1</dt><dd><div>321</div></dd></dl></div>", 1999.99m, "this is title 1", null },
                    { new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "this is description 2", 0.01, "<div class\"bd\"><p style=\"text-align:center\"><span>features 2</span><strong></strong></p></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>fee includes: 2</dt><dd><div>123</div></dd></dl></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>notes includes: 2</dt><dd><div>321</div></dd></dl></div>", 1999.99m, "this is title 2", null },
                    { new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "this is description 3", 0.01, "<div class\"bd\"><p style=\"text-align:center\"><span>features 3</span><strong></strong></p></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>fee includes: 3</dt><dd><div>123</div></dd></dl></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>notes includes: 3</dt><dd><div>321</div></dd></dl></div>", 1999.99m, "this is title 3", null },
                    { new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "this is description 4", 0.01, "<div class\"bd\"><p style=\"text-align:center\"><span>features 4</span><strong></strong></p></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>fee includes: 4</dt><dd><div>123</div></dd></dl></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>notes includes: 4</dt><dd><div>321</div></dd></dl></div>", 1999.99m, "this is title 4", null },
                    { new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "this is description 5", 0.01, "<div class\"bd\"><p style=\"text-align:center\"><span>features 5</span><strong></strong></p></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>fee includes: 5</dt><dd><div>123</div></dd></dl></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>notes includes: 5</dt><dd><div>321</div></dd></dl></div>", 1999.99m, "this is title 5", null },
                    { new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "this is description 6", 0.01, "<div class\"bd\"><p style=\"text-align:center\"><span>features 6</span><strong></strong></p></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>fee includes: 6</dt><dd><div>123</div></dd></dl></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>notes includes: 6</dt><dd><div>321</div></dd></dl></div>", 1999.99m, "this is title 6", null }
                });

            migrationBuilder.InsertData(
                table: "TouristRoutePictures",
                columns: new[] { "Id", "TouristRouteId", "Url" },
                values: new object[,]
                {
                    { 1, new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e1"), "../../assets/images/1.jpg" },
                    { 2, new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e1"), "../../assets/images/2.jpg" },
                    { 3, new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e1"), "../../assets/images/3.jpg" },
                    { 4, new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e1"), "../../assets/images/4.jpg" },
                    { 5, new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e2"), "../../assets/images/5.jpg" },
                    { 6, new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e3"), "../../assets/images/6.jpg" },
                    { 7, new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e4"), "../../assets/images/7.jpg" },
                    { 8, new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e5"), "../../assets/images/8.jpg" },
                    { 9, new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e6"), "../../assets/images/9.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TouristRoutePictures",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TouristRoutePictures",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TouristRoutePictures",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TouristRoutePictures",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TouristRoutePictures",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TouristRoutePictures",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TouristRoutePictures",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TouristRoutePictures",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TouristRoutePictures",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e1"));

            migrationBuilder.DeleteData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e2"));

            migrationBuilder.DeleteData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e3"));

            migrationBuilder.DeleteData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e4"));

            migrationBuilder.DeleteData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e5"));

            migrationBuilder.DeleteData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e6"));

            migrationBuilder.InsertData(
                table: "TouristRoutes",
                columns: new[] { "Id", "CreateTime", "DepartureTime", "Description", "DiscountPercent", "Features", "Fees", "Notes", "OriginalPrice", "Title", "UpdateTime" },
                values: new object[] { new Guid("0642a314-95f3-4093-9d81-217078b4eeaf"), new DateTime(2023, 5, 18, 1, 2, 39, 621, DateTimeKind.Utc).AddTicks(2640), null, "test description", null, null, null, null, 100m, "test title", null });
        }
    }
}
