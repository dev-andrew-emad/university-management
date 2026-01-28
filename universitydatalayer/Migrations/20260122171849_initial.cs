using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace universitydatalayer.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "instructors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instructors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    createdat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    hours = table.Column<int>(type: "int", nullable: false),
                    maxstudents = table.Column<int>(type: "int", nullable: false),
                    instructorid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courses", x => x.id);
                    table.ForeignKey(
                        name: "FK_courses_instructors_instructorid",
                        column: x => x.instructorid,
                        principalTable: "instructors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "studentcourses",
                columns: table => new
                {
                    studentid = table.Column<int>(type: "int", nullable: false),
                    courseid = table.Column<int>(type: "int", nullable: false),
                    enrolldate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    grade = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studentcourses", x => new { x.studentid, x.courseid });
                    table.ForeignKey(
                        name: "FK_studentcourses_courses_courseid",
                        column: x => x.courseid,
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_studentcourses_students_studentid",
                        column: x => x.studentid,
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "instructors",
                columns: new[] { "id", "email", "name" },
                values: new object[,]
                {
                    { 1, "ahmed@gamil.com", "ahmed hossam" },
                    { 2, "youssef@gmail.com", "youssef saad" }
                });

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "id", "age", "createdat", "email", "isactive", "name" },
                values: new object[,]
                {
                    { 1, 29, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "andrew@gmail.com", true, "andrew emad" },
                    { 2, 27, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "dina@gmail.com", true, "dina gamil" }
                });

            migrationBuilder.InsertData(
                table: "courses",
                columns: new[] { "id", "hours", "instructorid", "maxstudents", "name" },
                values: new object[,]
                {
                    { 1, 30, 1, 50, "english" },
                    { 2, 60, 2, 50, "math" }
                });

            migrationBuilder.InsertData(
                table: "studentcourses",
                columns: new[] { "courseid", "studentid", "enrolldate", "grade" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 1, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 1, 2, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 2, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_courses_instructorid",
                table: "courses",
                column: "instructorid");

            migrationBuilder.CreateIndex(
                name: "IX_studentcourses_courseid",
                table: "studentcourses",
                column: "courseid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "studentcourses");

            migrationBuilder.DropTable(
                name: "courses");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "instructors");
        }
    }
}
