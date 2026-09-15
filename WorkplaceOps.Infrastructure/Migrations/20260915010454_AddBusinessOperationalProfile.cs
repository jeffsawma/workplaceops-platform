using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkplaceOps.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBusinessOperationalProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessOperationalProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Industry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationCount = table.Column<int>(type: "int", nullable: false),
                    HasRemoteEmployees = table.Column<bool>(type: "bit", nullable: false),
                    HasUnionizedEmployees = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessOperationalProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessOperationalProfiles_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessOperationalProfiles_BusinessId",
                table: "BusinessOperationalProfiles",
                column: "BusinessId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessOperationalProfiles");
        }
    }
}
