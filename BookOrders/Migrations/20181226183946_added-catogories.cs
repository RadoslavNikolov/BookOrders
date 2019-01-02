using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookOrders.Migrations
{
    public partial class addedcatogories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 400, nullable: false),
                    NameNormalized = table.Column<string>(maxLength: 400, nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Identifier = table.Column<Guid>(nullable: false),
                    Descriprion = table.Column<string>(nullable: true),
                    Disabled = table.Column<bool>(nullable: false, defaultValue: false),
                    ParentId = table.Column<int>(nullable: true),
                    GroupId = table.Column<int>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    LastModifiedAtUtc = table.Column<DateTime>(nullable: true, defaultValueSql: "SYSUTCDATETIME()"),
                    LastModifiedId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.UniqueConstraint("AK_Categories_Identifier", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_Category_LastModifiedUser",
                        column: x => x.LastModifiedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_ParentCategory",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e3d84c70-65f2-4686-a243-912593c9c661", "44ab3373-a026-49dd-b066-b37a1960e119", "Admin", "ADMIN" },
                    { "03e411fd-33cf-481c-a4e7-5210406b06ae", "ed8e955a-350f-4105-b7ed-ea890f35081c", "PowerUser", "POWERUSER" },
                    { "f2794894-02bd-4968-8378-c2cb41e72812", "6ec588bd-e4f5-4589-b096-74894d8b6530", "User", "USER" },
                    { "2564cc2b-3d65-4953-90f5-51689a14c050", "ddca6342-c5ec-48f8-bfeb-ef72ecec09ee", "Guest", "GUEST" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_LastModifiedId",
                table: "Categories",
                column: "LastModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_NameNormalized",
                table: "Categories",
                column: "NameNormalized",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03e411fd-33cf-481c-a4e7-5210406b06ae");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2564cc2b-3d65-4953-90f5-51689a14c050");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3d84c70-65f2-4686-a243-912593c9c661");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f2794894-02bd-4968-8378-c2cb41e72812");

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
    }
}
