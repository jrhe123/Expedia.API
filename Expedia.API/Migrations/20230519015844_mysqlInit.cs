using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Expedia.API.Migrations
{
    /// <inheritdoc />
    public partial class mysqlInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TouristRoutes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(1500)", maxLength: 1500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OriginalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPercent = table.Column<double>(type: "double", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DepartureTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Features = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fees = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Rating = table.Column<double>(type: "double", nullable: true),
                    TravelDays = table.Column<int>(type: "int", nullable: true),
                    TripType = table.Column<int>(type: "int", nullable: true),
                    DepartureCity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristRoutes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TouristRoutePictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TouristRouteId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristRoutePictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TouristRoutePictures_TouristRoutes_TouristRouteId",
                        column: x => x.TouristRouteId,
                        principalTable: "TouristRoutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "TouristRoutes",
                columns: new[] { "Id", "CreateTime", "DepartureCity", "DepartureTime", "Description", "DiscountPercent", "Features", "Fees", "Notes", "OriginalPrice", "Rating", "Title", "TravelDays", "TripType", "UpdateTime" },
                values: new object[,]
                {
                    { new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, "this is description 1", 0.01, "<div class\"bd\"><p style=\"text-align:center\"><span>features </span><strong></strong></p></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>fee includes: 1</dt><dd><div>123</div></dd></dl></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>notes includes: 1</dt><dd><div>321</div></dd></dl></div>", 1999.99m, 3.5, "this is title 1", 8, 0, null },
                    { new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, "this is description 2", 0.01, "<div class\"bd\"><p style=\"text-align:center\"><span>features 2</span><strong></strong></p></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>fee includes: 2</dt><dd><div>123</div></dd></dl></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>notes includes: 2</dt><dd><div>321</div></dd></dl></div>", 1999.99m, 3.7999999999999998, "this is title 2", 8, 2, null },
                    { new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, "this is description 3", 0.01, "<div class\"bd\"><p style=\"text-align:center\"><span>features 3</span><strong></strong></p></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>fee includes: 3</dt><dd><div>123</div></dd></dl></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>notes includes: 3</dt><dd><div>321</div></dd></dl></div>", 1999.99m, 4.5, "this is title 3", 6, 2, null },
                    { new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, "this is description 4", 0.01, "<div class\"bd\"><p style=\"text-align:center\"><span>features 4</span><strong></strong></p></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>fee includes: 4</dt><dd><div>123</div></dd></dl></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>notes includes: 4</dt><dd><div>321</div></dd></dl></div>", 1999.99m, 5.0, "this is title 4", 5, 3, null },
                    { new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, "this is description 5", 0.01, "<div class\"bd\"><p style=\"text-align:center\"><span>features 5</span><strong></strong></p></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>fee includes: 5</dt><dd><div>123</div></dd></dl></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>notes includes: 5</dt><dd><div>321</div></dd></dl></div>", 1999.99m, 3.0, "this is title 5", 5, 0, null },
                    { new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, "this is description 6", 0.01, "<div class\"bd\"><p style=\"text-align:center\"><span>features 6</span><strong></strong></p></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>fee includes: 6</dt><dd><div>123</div></dd></dl></div>", "<div class\"bd\"><dl class=\"mod_info_box\"><dt>notes includes: 6</dt><dd><div>321</div></dd></dl></div>", 1999.99m, 4.5, "this is title 6", 3, 0, null }
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

            migrationBuilder.CreateIndex(
                name: "IX_TouristRoutePictures_TouristRouteId",
                table: "TouristRoutePictures",
                column: "TouristRouteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TouristRoutePictures");

            migrationBuilder.DropTable(
                name: "TouristRoutes");
        }
    }
}
