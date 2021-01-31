using Microsoft.EntityFrameworkCore.Migrations;

namespace Park2API.Migrations
{
    public partial class one : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StatusId",
                schema: "Identity",
                table: "Slots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Status",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Status",
                columns: new[] { "Id", "Description" },
                values: new object[] { "Reserved", "Slot reserved for internal use." });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Status",
                columns: new[] { "Id", "Description" },
                values: new object[] { "Available", "Slot available for external booking." });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Status",
                columns: new[] { "Id", "Description" },
                values: new object[] { "Hotel", "Slot reserved for Hotel use only." });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Status",
                schema: "Identity");

            migrationBuilder.DropColumn(
                name: "StatusId",
                schema: "Identity",
                table: "Slots");
        }
    }
}
