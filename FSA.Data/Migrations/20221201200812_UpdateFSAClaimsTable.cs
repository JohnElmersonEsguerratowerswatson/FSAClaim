using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSA.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFSAClaimsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isCancelled",
                table: "FSAClaims",
                type: "bit",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.AlterColumn<int>(
            //    name: "ID",
            //    table: "EmployeeFSAs",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_EmployeeFSAs",
            //    table: "EmployeeFSAs",
            //    column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_EmployeeFSAs",
            //    table: "EmployeeFSAs");

            migrationBuilder.DropColumn(
                name: "isCancelled",
                table: "FSAClaims");

            //migrationBuilder.AlterColumn<int>(
            //    name: "ID",
            //    table: "EmployeeFSAs",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
