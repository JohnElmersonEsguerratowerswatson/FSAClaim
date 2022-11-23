using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSA.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUsernameAndCreateIndices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Logins",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Logins_Username",
                table: "Logins",
                column: "Username");

            migrationBuilder.CreateIndex("IX_FSAClaim_Employee_ID", "FSAClaims", "EmployeeID");
            migrationBuilder.CreateIndex("IX_FSAClaim_ReferenceNumber", "FSAClaims", "ReferenceNumber");

        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Logins",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Logins");
        }
    }
}
