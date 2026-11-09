using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestuarantOrderSystem.Migrations
{
    /// <inheritdoc />
    public partial class FullDatabaseSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$gV1u64aGJOeuW6VwdML8leb9GGbnzhUp8MLrMabsAfebO/GM/pFiy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$yZD0BVSEgu.GxtYgVOaJtOmpz.wMfYKljk0DzshjPS2DHuYj0bNm.");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$DbbIv4aUDNAUHIQpPZDoHOY6Efjc4s4egsHfCvDKIkZD2DMw4PYaC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$/XZt1sXXpEro/4cOkWUcsejjJ/QlQGj8jyOscbl5f9fmwKirrWgkq");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$PzLukHlfGOsnNeTevbwux.Hcq0afm4TDSZgFrofeFLHoXR/pVayCy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$0HbbeL7yU84Rl1vpKcM7eeGt1D/lVy.NQmbHwjP4CjxjVPSKX7LUm");
        }
    }
}
