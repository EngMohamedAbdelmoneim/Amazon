using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Amazon.Core.IdentityDb.migrations
{
    /// <inheritdoc />
    public partial class AddSellerNameToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SellerName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellerName",
                table: "AspNetUsers");
        }
    }
}
