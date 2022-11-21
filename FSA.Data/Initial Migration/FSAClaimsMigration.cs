using FSA.Data.Entities;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FSA.Data.Initial_Migration
{
    internal class FSAClaimsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            //CREATE Employees Table
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: false, maxLength: 50),
                    LastName = table.Column<string>(nullable: false, maxLength: 50),
                    MiddleName = table.Column<string>(nullable: false, maxLength: 50)
                },
                constraints: table => table.PrimaryKey("PK_Employee", e => e.ID)

            );

            //CREATE FSARule Table
            migrationBuilder.CreateTable(
                name: "FSARule",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    FSALimit = table.Column<int>(nullable: false),
                    YearCoverage = table.Column<int>(nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_FSARule", f => f.ID)


            );

            //CREATE EmployeeFSA Table
            migrationBuilder.CreateTable(
                name: "EmployeeFSA",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(nullable: false),
                    FSAID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey("FK_EmployeeIDFSA", ef => ef.EmployeeID, "Employees");
                    table.ForeignKey("FK_FSARuleIDFSA", ef => ef.FSAID, "FSARule");
                }

            );


            //CREATE FSAClaims Table
            migrationBuilder.CreateTable(
                name: "FSAClaims",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false),
                    ClaimAmount = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ReceiptNumber = table.Column<int>(nullable: false),
                    ReceiptAmount = table.Column<int>(nullable: false),
                    ReceiptDate = table.Column<DateTime>(nullable: false),
                    DateSubmitted = table.Column<DateTime>(nullable: false),
                    ReferenceNumber = table.Column<int>(nullable: false),
                    ApprovalDate = table.Column<DateTime>(nullable: true),
                    Modified = table.Column<DateTime>(defaultValue: DateTime.UtcNow),
                },

                constraints: table =>
                {
                    table.PrimaryKey("PK_FSAClaims", fc => fc.ID);
                    table.ForeignKey("FK_EmployeeID_FSAClaim", fc => fc.EmployeeID, "Employees");
                }
            );

            //CREATE Login Table
            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    EmplyeeID = table.Column<int>(nullable: false),
                    Password = table.Column<string>(maxLength: 16, nullable: false),
                    Role = table.Column<string>(nullable: false, defaultValue: "User")
                },
                constraints: table =>
                {
                    table.ForeignKey("FK_EmployeeID_Login", l => l.EmplyeeID, "Employees");
                }
            );

        }
    }
}
