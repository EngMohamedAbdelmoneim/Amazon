using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Amazon.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddSellerEmailToOrderItemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Product_SellerEmail",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Product_SellerEmail",
                table: "OrderItems");
        }
    }
}
