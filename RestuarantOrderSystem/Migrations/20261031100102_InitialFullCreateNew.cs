using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestuarantOrderSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialFullCreateNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$/XZt1sXXpEro/4cOkWUcsejjJ/QlQGj8jyOscbl5f9fmwKirrWgkq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$PzLukHlfGOsnNeTevbwux.Hcq0afm4TDSZgFrofeFLHoXR/pVayCy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$0HbbeL7yU84Rl1vpKcM7eeGt1D/lVy.NQmbHwjP4CjxjVPSKX7LUm" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 10, 31, 9, 52, 25, 755, DateTimeKind.Utc).AddTicks(9865), "$2a$11$ntdkIGibRolkLAP9DVe9G.PJ4sFa1wnGQDuvZAsTBIf76cZ5YEj4." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 10, 31, 9, 52, 25, 863, DateTimeKind.Utc).AddTicks(201), "$2a$11$y6Y7RlgWDonuQ.d1QefC2uPRjGVnXFCCmHTabWpt92BgU/lkwI9rq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 10, 31, 9, 52, 25, 971, DateTimeKind.Utc).AddTicks(4722), "$2a$11$C9cI0ok7yI8njVEmaT.No.27WWe/MZCVnz31cQImndXq5l5jrlNaK" });
        }
    }
}
