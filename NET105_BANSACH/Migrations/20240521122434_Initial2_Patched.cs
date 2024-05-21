using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NET105_BANSACH.Migrations
{
    /// <inheritdoc />
    public partial class Initial2_Patched : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "CartsDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductID",
                table: "CartsDetails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
