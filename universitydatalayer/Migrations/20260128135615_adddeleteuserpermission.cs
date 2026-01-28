using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace universitydatalayer.Migrations
{
    /// <inheritdoc />
    public partial class adddeleteuserpermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id", "name" },
                values: new object[] { 4, "deleteuser" });

            migrationBuilder.InsertData(
                table: "userpermissions",
                columns: new[] { "permissionid", "userid" },
                values: new object[] { 4, 3 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "userpermissions",
                keyColumns: new[] { "permissionid", "userid" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 4);
        }
    }
}
