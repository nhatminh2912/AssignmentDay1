using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCoreAssignmentDay1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<float>(
                name: "Amount",
                table: "Salaries",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Project_Employee",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Departments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0d94a58c-9233-4ffb-9ddd-c17f78e6dbbc"), "HR" },
                    { new Guid("75fd321b-0296-48a7-807b-e4da2ba24090"), "Accountant" },
                    { new Guid("d20e766e-2cbb-403b-8e15-ec262d3a9523"), "Finance" },
                    { new Guid("ec4b43f0-1ad9-4462-be99-c4b8a998edde"), "Software Development" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("611b1df4-a602-4bcd-a1b2-37856963c119"), "Project A" },
                    { new Guid("d5352d56-261f-4433-b502-d8984a152324"), "Project B" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DepartmentId", "JoinedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("4168392f-dc02-4d7a-8ea5-cc6f85d1a3ca"), new Guid("ec4b43f0-1ad9-4462-be99-c4b8a998edde"), new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "John Doe" },
                    { new Guid("7b707dc4-68b4-4dce-b9d9-676845178909"), new Guid("d20e766e-2cbb-403b-8e15-ec262d3a9523"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane Smith" }
                });

            migrationBuilder.InsertData(
                table: "Project_Employee",
                columns: new[] { "EmployeeId", "ProjectId", "Enable", "Id" },
                values: new object[,]
                {
                    { new Guid("4168392f-dc02-4d7a-8ea5-cc6f85d1a3ca"), new Guid("611b1df4-a602-4bcd-a1b2-37856963c119"), true, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("7b707dc4-68b4-4dce-b9d9-676845178909"), new Guid("d5352d56-261f-4433-b502-d8984a152324"), true, new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Salaries",
                columns: new[] { "Id", "Amount", "EmployeeId" },
                values: new object[,]
                {
                    { new Guid("0c60d053-90f7-4000-9316-1085df7b4f9b"), 2000f, new Guid("7b707dc4-68b4-4dce-b9d9-676845178909") },
                    { new Guid("b2429578-2a05-4a70-8814-6feb31e3f8ba"), 1500f, new Guid("4168392f-dc02-4d7a-8ea5-cc6f85d1a3ca") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("0d94a58c-9233-4ffb-9ddd-c17f78e6dbbc"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("75fd321b-0296-48a7-807b-e4da2ba24090"));

            migrationBuilder.DeleteData(
                table: "Project_Employee",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("4168392f-dc02-4d7a-8ea5-cc6f85d1a3ca"), new Guid("611b1df4-a602-4bcd-a1b2-37856963c119") });

            migrationBuilder.DeleteData(
                table: "Project_Employee",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { new Guid("7b707dc4-68b4-4dce-b9d9-676845178909"), new Guid("d5352d56-261f-4433-b502-d8984a152324") });

            migrationBuilder.DeleteData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: new Guid("0c60d053-90f7-4000-9316-1085df7b4f9b"));

            migrationBuilder.DeleteData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: new Guid("b2429578-2a05-4a70-8814-6feb31e3f8ba"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("4168392f-dc02-4d7a-8ea5-cc6f85d1a3ca"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("7b707dc4-68b4-4dce-b9d9-676845178909"));

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: new Guid("611b1df4-a602-4bcd-a1b2-37856963c119"));

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: new Guid("d5352d56-261f-4433-b502-d8984a152324"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("d20e766e-2cbb-403b-8e15-ec262d3a9523"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("ec4b43f0-1ad9-4462-be99-c4b8a998edde"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Project_Employee");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Salaries",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

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
        }
    }
}
