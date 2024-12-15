using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wypozyczalnia_samochodowa.Migrations
{
    /// <inheritdoc />
    public partial class AddNumerRejestracyjny : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumerRejestracyjny",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumerRejestracyjny",
                table: "Cars");
        }
    }
}
