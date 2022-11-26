using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSA.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployeeFSAs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "EmployeeFSAs",
                type: "int",
                nullable: false,
                defaultValue: 0).Annotation("SqlServer:Identity", "1, 1");
            migrationBuilder.AddPrimaryKey("PK_EmployeeFSAs", "EmployeeFSAs", "ID");
            migrationBuilder.AddForeignKey("FK_Employee_FSA_Employee_ID", "EmployeeFSAs", "EmployeeID", "Employees");
            migrationBuilder.AddForeignKey("FK_FSARule_FSA_FSARule_ID", "EmployeeFSAs", "FSAID", "FSARules");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID",
                table: "EmployeeFSAs");
        }
    }
}
