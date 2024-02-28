using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BangazonBE.Migrations
{
    public partial class ImageUrlImplementation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://images.thdstatic.com/productImages/45192fb2-63e1-429d-a340-eea0644cadec/svn/stainless-steel-whirlpool-over-the-range-microwaves-wmh31017hs-64_600.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://cdn.thewirecutter.com/wp-content/media/2023/11/mechanicalkeyboards-2048px-9138.jpg?auto=webp&quality=75&crop=1.91:1&width=1200");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/I/714kO41XjDL.jpg");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 2, 27, 20, 25, 49, 343, DateTimeKind.Local).AddTicks(7174));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "blankfornow");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "blankfornow");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "blankfornow");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 2, 27, 18, 36, 43, 568, DateTimeKind.Local).AddTicks(195));
        }
    }
}
