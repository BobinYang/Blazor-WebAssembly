using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Covid.Server.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DepartmentId = table.Column<int>(nullable: false),
                    No = table.Column<string>(maxLength: 3, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PictureUrl = table.Column<string>(maxLength: 500, nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DailyHealths",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    HealthCondition = table.Column<int>(nullable: false),
                    Temperature = table.Column<float>(nullable: false),
                    Remark = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyHealths", x => new { x.EmployeeId, x.Date });
                    table.ForeignKey(
                        name: "FK_DailyHealths_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "研发部" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "销售部" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "采购部" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BirthDate", "DepartmentId", "Gender", "Name", "No", "PictureUrl" },
                values: new object[] { 1, new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, "Nick Carter", "A01", "" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BirthDate", "DepartmentId", "Gender", "Name", "No", "PictureUrl" },
                values: new object[] { 2, new DateTime(1975, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, "Mike Seaver", "A02", "" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BirthDate", "DepartmentId", "Gender", "Name", "No", "PictureUrl" },
                values: new object[] { 3, new DateTime(1989, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, "Sarah Jackson", "B01", "" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BirthDate", "DepartmentId", "Gender", "Name", "No", "PictureUrl" },
                values: new object[] { 4, new DateTime(1995, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, "Mary Bloody", "B02", "" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BirthDate", "DepartmentId", "Gender", "Name", "No", "PictureUrl" },
                values: new object[] { 5, new DateTime(1979, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, "Joe Kent", "C01", "" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BirthDate", "DepartmentId", "Gender", "Name", "No", "PictureUrl" },
                values: new object[] { 6, new DateTime(1961, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, "Axl", "C02", "" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyHealths");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
