using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSA.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFSAClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropIndex("IX_FSAClaim_ReferenceNumber", "FSAClaims");

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceNumber",
                table: "FSAClaims",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
            //migrationBuilder.CreateIndex("IX_FSAClaim_ReferenceNumber", "FSAClaims", "ReferenceNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ReferenceNumber",
                table: "FSAClaims",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}