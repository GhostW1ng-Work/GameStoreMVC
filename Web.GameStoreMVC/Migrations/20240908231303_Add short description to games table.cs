using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.GameStoreMVC.Migrations
{
    /// <inheritdoc />
    public partial class Addshortdescriptiontogamestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Games");
        }
    }
}
