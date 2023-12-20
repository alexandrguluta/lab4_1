using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buyers",
                columns: table => new
                {
                    BuyerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyers", x => x.BuyerID);
                    table.ForeignKey(
                        name: "FK_Buyers_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "BuyerID");
                });

            migrationBuilder.CreateTable(
                name: "Consultants",
                columns: table => new
                {
                    ConsultantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsultantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultants", x => x.ConsultantID);
                    table.ForeignKey(
                        name: "FK_Consultants_Consultants_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "Consultants",
                        principalColumn: "ConsultantID");
                });

            migrationBuilder.InsertData(
                table: "Buyers",
                columns: new[] { "BuyerID", "Address", "BuyerId", "Country", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("4d490a70-94ce-4d15-9494-5248280c2ce3"), "Saransk, Veselovskogo20k1", null, "Russia", "Stas", "86743643904" },
                    { new Guid("c2d4c053-49b6-410c-bc78-2d54a9991870"), "Saransk, Veselovskogo20k2", null, "Russia", "Nikita", "89530309776" }
                });

            migrationBuilder.InsertData(
                table: "Consultants",
                columns: new[] { "ConsultantID", "Address", "ConsultantId", "Name", "Number", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d45-9494-5248280c2ce3"), "Saransk, Nevskogo 113", null, "Genadiy", "2", "82002769076" },
                    { new Guid("c9d4c053-49b6-430c-bc78-2d54a9991870"), "Saransk, Nevskogo 11", null, "Andrey", "1", "85882507000" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buyers_BuyerId",
                table: "Buyers",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultants_ConsultantId",
                table: "Consultants",
                column: "ConsultantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buyers");

            migrationBuilder.DropTable(
                name: "Consultants");
        }
    }
}
