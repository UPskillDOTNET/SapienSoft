using Microsoft.EntityFrameworkCore.Migrations;

namespace iParkMedusa.Migrations
{
    public partial class medusa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "Parks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Uri" },
                values: new object[] { "Park", "https://localhost:44365/" });

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "Parks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "Uri" },
                values: new object[] { "Pax", "https://localhost:44355/" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "Parks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Uri" },
                values: new object[] { "Park1", null });

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "Parks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "Uri" },
                values: new object[] { "Park2", null });
        }
    }
}
