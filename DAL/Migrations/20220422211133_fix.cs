using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_Countryid",
                table: "Cities");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Countries",
                newName: "CountryID");

            migrationBuilder.RenameColumn(
                name: "Countryid",
                table: "Cities",
                newName: "CountryID");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_Countryid",
                table: "Cities",
                newName: "IX_Cities_CountryID");

            migrationBuilder.AlterColumn<int>(
                name: "CountryID",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryID",
                table: "Cities",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryID",
                table: "Cities");

            migrationBuilder.RenameColumn(
                name: "CountryID",
                table: "Countries",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CountryID",
                table: "Cities",
                newName: "Countryid");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_CountryID",
                table: "Cities",
                newName: "IX_Cities_Countryid");

            migrationBuilder.AlterColumn<int>(
                name: "Countryid",
                table: "Cities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_Countryid",
                table: "Cities",
                column: "Countryid",
                principalTable: "Countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
