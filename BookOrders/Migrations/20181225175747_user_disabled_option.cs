using Microsoft.EntityFrameworkCore.Migrations;

namespace BookOrders.Migrations
{
    public partial class user_disabled_option : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "768b3554-b2a8-41ef-91e8-e2854a993b26");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aabc626c-d575-46a6-a940-82f97e470aac");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c25e568a-c595-4fb8-bc75-3ac33e4910de");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce7ec33c-660a-468e-813e-5032c7c16e57");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AddColumn<bool>(
                name: "Disabled",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5db65654-96bc-4cb5-be28-c47f9b3683c7", "49a90c21-9cc7-4bc4-8263-438753590908", "Admin", "ADMIN" },
                    { "23e0f93c-0c4a-4b71-82d9-0da0d2adfb00", "2c86d497-7770-4abb-9cdf-07d564ea3316", "PowerUser", "POWERUSER" },
                    { "3f06ff17-2e7e-498f-a377-e9d8bddfdf93", "8b4e3aae-1680-461a-a6a9-85430102de39", "User", "USER" },
                    { "8b439158-4a68-4a4e-83a3-c9aed3037d03", "4030424c-9242-431f-969a-b632fee4f4d9", "Guest", "GUEST" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23e0f93c-0c4a-4b71-82d9-0da0d2adfb00");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f06ff17-2e7e-498f-a377-e9d8bddfdf93");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5db65654-96bc-4cb5-be28-c47f9b3683c7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b439158-4a68-4a4e-83a3-c9aed3037d03");

            migrationBuilder.DropColumn(
                name: "Disabled",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ce7ec33c-660a-468e-813e-5032c7c16e57", "a511b6fa-2610-4a02-be05-44b79ad3e0d0", "Admin", "ADMIN" },
                    { "768b3554-b2a8-41ef-91e8-e2854a993b26", "874eecc8-ec54-4a97-8cfb-e00bec27496a", "PowerUser", "POWERUSER" },
                    { "c25e568a-c595-4fb8-bc75-3ac33e4910de", "2516153b-0800-4255-81d6-90a18afea682", "User", "USER" },
                    { "aabc626c-d575-46a6-a940-82f97e470aac", "73b28ef9-d499-47e0-959e-dd76678d2586", "Guest", "GUEST" }
                });
        }
    }
}
