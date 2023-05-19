using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expedia.API.Migrations
{
    /// <inheritdoc />
    public partial class updateTouristRouteSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartureCity",
                table: "TouristRoutes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "TouristRoutes",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TravelDays",
                table: "TouristRoutes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TripType",
                table: "TouristRoutes",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e1"),
                columns: new[] { "DepartureCity", "Rating", "TravelDays", "TripType" },
                values: new object[] { 0, 3.5, 8, 0 });

            migrationBuilder.UpdateData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e2"),
                columns: new[] { "DepartureCity", "Rating", "TravelDays", "TripType" },
                values: new object[] { 1, 3.7999999999999998, 8, 2 });

            migrationBuilder.UpdateData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e3"),
                columns: new[] { "DepartureCity", "Rating", "TravelDays", "TripType" },
                values: new object[] { 1, 4.5, 6, 2 });

            migrationBuilder.UpdateData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e4"),
                columns: new[] { "DepartureCity", "Rating", "TravelDays", "TripType" },
                values: new object[] { 3, 5.0, 5, 3 });

            migrationBuilder.UpdateData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e5"),
                columns: new[] { "DepartureCity", "Rating", "TravelDays", "TripType" },
                values: new object[] { 3, 3.0, 5, 0 });

            migrationBuilder.UpdateData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e6"),
                columns: new[] { "DepartureCity", "Rating", "TravelDays", "TripType" },
                values: new object[] { 3, 4.5, 3, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartureCity",
                table: "TouristRoutes");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "TouristRoutes");

            migrationBuilder.DropColumn(
                name: "TravelDays",
                table: "TouristRoutes");

            migrationBuilder.DropColumn(
                name: "TripType",
                table: "TouristRoutes");
        }
    }
}
