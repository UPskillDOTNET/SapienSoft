using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Park2API.Migrations
{
    public partial class two : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.CreateTable(
                name: "Discounts",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeekDay = table.Column<int>(type: "int", nullable: false),
                    Hour = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(16,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Slots",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PricePerHour = table.Column<decimal>(type: "decimal(16,4)", nullable: false),
                    StatusId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(16,4)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SlotId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Slots_SlotId",
                        column: x => x.SlotId,
                        principalSchema: "Identity",
                        principalTable: "Slots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "Identity",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Discounts",
                columns: new[] { "Id", "Hour", "Rate", "WeekDay" },
                values: new object[,]
                {
                    { 1, 0, 1.0m, 1 },
                    { 109, 12, 1.0m, 5 },
                    { 110, 13, 1.0m, 5 },
                    { 111, 14, 1.0m, 5 },
                    { 112, 15, 1.0m, 5 },
                    { 113, 16, 1.0m, 5 },
                    { 114, 17, 1.0m, 5 },
                    { 115, 18, 1.0m, 5 },
                    { 116, 19, 1.0m, 5 },
                    { 117, 20, 1.0m, 5 },
                    { 118, 21, 1.0m, 5 },
                    { 119, 22, 1.0m, 5 },
                    { 120, 23, 1.0m, 5 },
                    { 121, 0, 1.0m, 6 },
                    { 122, 1, 1.0m, 6 },
                    { 123, 2, 1.0m, 6 },
                    { 124, 3, 1.0m, 6 },
                    { 125, 4, 1.0m, 6 },
                    { 108, 11, 1.0m, 5 },
                    { 126, 5, 1.0m, 6 },
                    { 107, 10, 1.0m, 5 },
                    { 105, 8, 1.0m, 5 },
                    { 87, 14, 1.0m, 4 },
                    { 88, 15, 1.0m, 4 },
                    { 89, 16, 1.0m, 4 },
                    { 90, 17, 1.0m, 4 },
                    { 92, 19, 1.0m, 4 },
                    { 93, 20, 1.0m, 4 },
                    { 94, 21, 1.0m, 4 },
                    { 95, 22, 1.0m, 4 },
                    { 96, 23, 1.0m, 4 },
                    { 97, 0, 1.0m, 5 },
                    { 98, 1, 1.0m, 5 },
                    { 99, 2, 1.0m, 5 },
                    { 100, 3, 1.0m, 5 },
                    { 101, 4, 1.0m, 5 },
                    { 102, 5, 1.0m, 5 },
                    { 103, 6, 1.0m, 5 },
                    { 104, 7, 1.0m, 5 },
                    { 106, 9, 1.0m, 5 },
                    { 127, 6, 1.0m, 6 },
                    { 128, 7, 1.0m, 6 }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Discounts",
                columns: new[] { "Id", "Hour", "Rate", "WeekDay" },
                values: new object[,]
                {
                    { 129, 8, 1.0m, 6 },
                    { 152, 7, 1.0m, 0 },
                    { 153, 8, 1.0m, 0 },
                    { 154, 9, 1.0m, 0 },
                    { 155, 10, 1.0m, 0 },
                    { 156, 11, 1.0m, 0 },
                    { 157, 12, 1.0m, 0 },
                    { 158, 13, 1.0m, 0 },
                    { 159, 14, 1.0m, 0 },
                    { 160, 15, 1.0m, 0 },
                    { 161, 16, 1.0m, 0 },
                    { 162, 17, 1.0m, 0 },
                    { 163, 18, 1.0m, 0 },
                    { 164, 19, 1.0m, 0 },
                    { 165, 20, 1.0m, 0 },
                    { 166, 21, 1.0m, 0 },
                    { 167, 22, 1.0m, 0 },
                    { 168, 23, 1.0m, 0 },
                    { 151, 6, 1.0m, 0 },
                    { 150, 5, 1.0m, 0 },
                    { 149, 4, 1.0m, 0 },
                    { 148, 3, 1.0m, 0 },
                    { 130, 9, 1.0m, 6 },
                    { 131, 10, 1.0m, 6 },
                    { 132, 11, 1.0m, 6 },
                    { 133, 12, 1.0m, 6 },
                    { 134, 13, 1.0m, 6 },
                    { 135, 14, 1.0m, 6 },
                    { 136, 15, 1.0m, 6 },
                    { 137, 16, 1.0m, 6 },
                    { 86, 13, 1.0m, 4 },
                    { 138, 17, 1.0m, 6 },
                    { 140, 19, 1.0m, 6 },
                    { 141, 20, 1.0m, 6 },
                    { 142, 21, 1.0m, 6 },
                    { 143, 22, 1.0m, 6 },
                    { 144, 23, 1.0m, 6 },
                    { 145, 0, 1.0m, 0 },
                    { 146, 1, 1.0m, 0 },
                    { 147, 2, 1.0m, 0 },
                    { 139, 18, 1.0m, 6 },
                    { 85, 12, 1.0m, 4 }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Discounts",
                columns: new[] { "Id", "Hour", "Rate", "WeekDay" },
                values: new object[,]
                {
                    { 91, 18, 1.0m, 4 },
                    { 83, 10, 1.0m, 4 },
                    { 24, 23, 1.0m, 1 },
                    { 25, 0, 1.0m, 2 },
                    { 26, 1, 1.0m, 2 },
                    { 27, 2, 1.0m, 2 },
                    { 28, 3, 1.0m, 2 },
                    { 29, 4, 1.0m, 2 },
                    { 30, 5, 1.0m, 2 },
                    { 31, 6, 1.0m, 2 },
                    { 32, 7, 1.0m, 2 },
                    { 33, 8, 1.0m, 2 },
                    { 34, 9, 1.0m, 2 },
                    { 35, 10, 1.0m, 2 },
                    { 36, 11, 1.0m, 2 },
                    { 84, 11, 1.0m, 4 },
                    { 38, 13, 1.0m, 2 },
                    { 39, 14, 1.0m, 2 },
                    { 40, 15, 1.0m, 2 },
                    { 23, 22, 1.0m, 1 },
                    { 22, 21, 1.0m, 1 },
                    { 21, 20, 1.0m, 1 },
                    { 20, 19, 1.0m, 1 },
                    { 2, 1, 1.0m, 1 },
                    { 3, 2, 1.0m, 1 },
                    { 4, 3, 1.0m, 1 },
                    { 5, 4, 1.0m, 1 },
                    { 6, 5, 1.0m, 1 },
                    { 7, 6, 1.0m, 1 },
                    { 8, 7, 1.0m, 1 },
                    { 9, 8, 1.0m, 1 },
                    { 41, 16, 1.0m, 2 },
                    { 10, 9, 1.0m, 1 },
                    { 12, 11, 1.0m, 1 },
                    { 13, 12, 1.0m, 1 },
                    { 14, 13, 1.0m, 1 },
                    { 15, 14, 1.0m, 1 },
                    { 16, 15, 1.0m, 1 },
                    { 17, 16, 1.0m, 1 },
                    { 18, 17, 1.0m, 1 },
                    { 19, 18, 1.0m, 1 },
                    { 11, 10, 1.0m, 1 }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Discounts",
                columns: new[] { "Id", "Hour", "Rate", "WeekDay" },
                values: new object[,]
                {
                    { 42, 17, 1.0m, 2 },
                    { 37, 12, 1.0m, 2 },
                    { 44, 19, 1.0m, 2 },
                    { 66, 17, 1.0m, 3 },
                    { 67, 18, 1.0m, 3 },
                    { 68, 19, 1.0m, 3 },
                    { 69, 20, 1.0m, 3 },
                    { 43, 18, 1.0m, 2 },
                    { 71, 22, 1.0m, 3 },
                    { 72, 23, 1.0m, 3 },
                    { 73, 0, 1.0m, 4 },
                    { 74, 1, 1.0m, 4 },
                    { 75, 2, 1.0m, 4 },
                    { 76, 3, 1.0m, 4 },
                    { 77, 4, 1.0m, 4 },
                    { 78, 5, 1.0m, 4 },
                    { 79, 6, 1.0m, 4 },
                    { 80, 7, 1.0m, 4 },
                    { 81, 8, 1.0m, 4 },
                    { 82, 9, 1.0m, 4 },
                    { 65, 16, 1.0m, 3 },
                    { 64, 15, 1.0m, 3 },
                    { 70, 21, 1.0m, 3 },
                    { 62, 13, 1.0m, 3 },
                    { 45, 20, 1.0m, 2 },
                    { 46, 21, 1.0m, 2 },
                    { 47, 22, 1.0m, 2 },
                    { 63, 14, 1.0m, 3 },
                    { 49, 0, 1.0m, 3 },
                    { 50, 1, 1.0m, 3 },
                    { 51, 2, 1.0m, 3 },
                    { 52, 3, 1.0m, 3 },
                    { 48, 23, 1.0m, 2 },
                    { 54, 5, 1.0m, 3 },
                    { 55, 6, 1.0m, 3 },
                    { 56, 7, 1.0m, 3 },
                    { 57, 8, 1.0m, 3 },
                    { 58, 9, 1.0m, 3 },
                    { 59, 10, 1.0m, 3 },
                    { 60, 11, 1.0m, 3 },
                    { 61, 12, 1.0m, 3 },
                    { 53, 4, 1.0m, 3 }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Slots",
                columns: new[] { "Id", "PricePerHour", "Status", "StatusId" },
                values: new object[,]
                {
                    { "B05", 1.90m, "Available", null },
                    { "B04", 1.90m, "Available", null },
                    { "B03", 1.90m, "Available", null },
                    { "B02", 0.50m, "Reserved", null },
                    { "B01", 0.50m, "Hotel", null },
                    { "A02", 0m, "Reserved", null },
                    { "A04", 2.15m, "Available", null },
                    { "A03", 2.15m, "Available", null },
                    { "A01", 0m, "Reserved", null },
                    { "A05", 2.15m, "Available", null }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Status",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { "Available", "Slot available for external booking." },
                    { "Reserved", "Slot reserved for internal use." },
                    { "Hotel", "Slot reserved for Hotel use only." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SlotId",
                schema: "Identity",
                table: "Reservations",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                schema: "Identity",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Identity",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "Identity",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Identity",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Identity",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "Identity",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "Identity",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "Identity",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discounts",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Reservations",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Status",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Slots",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Identity");
        }
    }
}
