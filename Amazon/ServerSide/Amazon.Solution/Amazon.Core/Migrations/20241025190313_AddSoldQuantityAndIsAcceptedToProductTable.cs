using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Amazon.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddSoldQuantityAndIsAcceptedToProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "QuantitySold",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsAccepted", "QuantitySold" },
                values: new object[] { true, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsAccepted", "QuantitySold" },
                values: new object[] { true, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IsAccepted", "QuantitySold" },
                values: new object[] { true, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "IsAccepted", "QuantitySold" },
                values: new object[] { true, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "IsAccepted", "QuantitySold" },
                values: new object[] { true, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "IsAccepted", "QuantitySold" },
                values: new object[] { true, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "IsAccepted", "QuantitySold" },
                values: new object[] { true, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "IsAccepted", "QuantitySold" },
                values: new object[] { true, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "IsAccepted", "QuantitySold" },
                values: new object[] { true, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "IsAccepted", "QuantitySold" },
                values: new object[] { true, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "IsAccepted", "QuantitySold" },
                values: new object[] { true, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "IsAccepted", "QuantitySold" },
                values: new object[] { true, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "QuantitySold",
                table: "Products");
        }
    }
}
