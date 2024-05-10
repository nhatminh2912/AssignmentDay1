using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCoreAssignmentDay1.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEmployees_Employees_EmployeeId",
                table: "ProjectEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEmployees_Projects_ProjectId",
                table: "ProjectEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectEmployees",
                table: "ProjectEmployees");

            migrationBuilder.RenameTable(
                name: "ProjectEmployees",
                newName: "Project_Employee");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectEmployees_EmployeeId",
                table: "Project_Employee",
                newName: "IX_Project_Employee_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project_Employee",
                table: "Project_Employee",
                columns: new[] { "ProjectId", "EmployeeId" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2dae287e-93f2-4639-b8ed-9fc5ea650330"), "HR" },
                    { new Guid("5788501c-5201-40bd-8f88-7a819980bc24"), "Software Development" },
                    { new Guid("843ac3ed-6143-46f2-b855-0515b9fa0b14"), "Accounting" },
                    { new Guid("bd377863-9329-494c-9649-ab0710a70c4a"), "Finance" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Employee_Employees_EmployeeId",
                table: "Project_Employee",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Employee_Projects_ProjectId",
                table: "Project_Employee",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Employee_Employees_EmployeeId",
                table: "Project_Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Employee_Projects_ProjectId",
                table: "Project_Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project_Employee",
                table: "Project_Employee");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("2dae287e-93f2-4639-b8ed-9fc5ea650330"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("5788501c-5201-40bd-8f88-7a819980bc24"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("843ac3ed-6143-46f2-b855-0515b9fa0b14"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("bd377863-9329-494c-9649-ab0710a70c4a"));

            migrationBuilder.RenameTable(
                name: "Project_Employee",
                newName: "ProjectEmployees");

            migrationBuilder.RenameIndex(
                name: "IX_Project_Employee_EmployeeId",
                table: "ProjectEmployees",
                newName: "IX_ProjectEmployees_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectEmployees",
                table: "ProjectEmployees",
                columns: new[] { "ProjectId", "EmployeeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectEmployees_Employees_EmployeeId",
                table: "ProjectEmployees",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectEmployees_Projects_ProjectId",
                table: "ProjectEmployees",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
