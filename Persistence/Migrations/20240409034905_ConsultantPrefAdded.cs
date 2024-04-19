using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ConsultantPrefAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsultantPreferences",
                columns: table => new
                {
                    ConsultantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    AppointmentInterval = table.Column<int>(type: "int", nullable: false),
                    LunchBreakStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    LunchBreakEndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    DaysAheadForAppointment = table.Column<int>(type: "int", nullable: false),
                    IsWeekendAppointmentAllowed = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultantPreferences", x => x.ConsultantId);
                    table.ForeignKey(
                        name: "FK_ConsultantPreferences_Consultants_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "Consultants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("682bd438-dac9-485e-9eea-d1e506f96ae6"),
                column: "ConcurrencyStamp",
                value: "831fe910-fe61-433c-9551-8f35385fde47");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7459c39b-7569-41ed-9e20-523420e88247"),
                column: "ConcurrencyStamp",
                value: "659e8c05-0ce3-4291-9c14-cbc96194abb5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a4ad46b1-5cba-46fa-a804-0b81773b8ff0"),
                column: "ConcurrencyStamp",
                value: "d5b3a6c8-ccda-4823-b01f-d8a612db1567");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9f81f2ca-0dc5-4bb6-b8ea-e60f296b5231"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e7b20ab-c645-41b9-889b-eb0339436fb0", new DateTime(2024, 4, 9, 3, 49, 4, 986, DateTimeKind.Utc).AddTicks(5110), "AQAAAAIAAYagAAAAEAoiFhcSAWMuF7EUd0mMWTHiMpvwsKuPOOR4kqnkTKeKaa2XZyzW1FBLL2n7z3Vy3w==", "b4074c05-ad3a-402d-8dad-38af7c153ab2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cf6db848-b71b-4bb9-b37a-bd6e11f90f60"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5e5d0b5-e68f-435c-9a29-08c3db5c504e", new DateTime(2024, 4, 9, 3, 49, 5, 27, DateTimeKind.Utc).AddTicks(8040), "AQAAAAIAAYagAAAAELLE7diDpVEyJc/3yzXehj14BYyIHpYByRSYQnRfDzuYGlP641adk3P2gOKe+3Je7w==", "7dc3bee1-2d1e-4596-a69f-93a80e91ae50" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsultantPreferences");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("682bd438-dac9-485e-9eea-d1e506f96ae6"),
                column: "ConcurrencyStamp",
                value: "1af0c72f-ccf9-4a53-a8e3-50a0350ba9ba");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7459c39b-7569-41ed-9e20-523420e88247"),
                column: "ConcurrencyStamp",
                value: "8440f5d7-706f-4118-a441-69bada2ee43e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a4ad46b1-5cba-46fa-a804-0b81773b8ff0"),
                column: "ConcurrencyStamp",
                value: "e238ffc9-950a-4fec-b4cd-890275244bf8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9f81f2ca-0dc5-4bb6-b8ea-e60f296b5231"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d60a44d7-a378-4bff-aacd-49110303ca82", new DateTime(2024, 4, 7, 14, 47, 1, 212, DateTimeKind.Utc).AddTicks(1850), "AQAAAAIAAYagAAAAEAXwqtuG/ofpa02zfmeHb/yNaDHXmprS+lwRQxYlTMMStXVt8uUCqC21Camn247XgA==", "e3e4a3d5-a7ee-4856-ad8d-3cb473d8e815" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cf6db848-b71b-4bb9-b37a-bd6e11f90f60"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7a61e517-d620-42d7-82c9-0c1198819d4f", new DateTime(2024, 4, 7, 14, 47, 1, 253, DateTimeKind.Utc).AddTicks(4700), "AQAAAAIAAYagAAAAEDmrkUpGUgzuob64KLyII5NuviQYEyI3tnuCquLsCJ1v1iO0x8+24nYQLXd32yTSuA==", "ac177e96-f37f-4826-8da2-3c497183d473" });
        }
    }
}
