using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSA.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignkeyEmployeeClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey("FK_FSAClaim_Employee_ID", "FSAClaims", "EmployeeID","Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_FSAClaim_Employee_ID", "FSAClaims");
        }
    }
}
