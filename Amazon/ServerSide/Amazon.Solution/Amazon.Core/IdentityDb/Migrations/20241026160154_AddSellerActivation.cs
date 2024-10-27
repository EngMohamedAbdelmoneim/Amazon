using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Amazon.Core.IdentityDb.migrations
{
    /// <inheritdoc />
    public partial class AddSellerActivation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActiveSeller",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActiveSeller",
                table: "AspNetUsers");
        }
    }
}
