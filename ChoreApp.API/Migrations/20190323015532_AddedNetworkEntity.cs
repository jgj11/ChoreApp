using Microsoft.EntityFrameworkCore.Migrations;

namespace ChoreApp.API.Migrations
{
    public partial class AddedNetworkEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Networks",
                columns: table => new
                {
                    NetworkerId = table.Column<int>(nullable: false),
                    NetworkeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Networks", x => new { x.NetworkerId, x.NetworkeeId });
                    table.ForeignKey(
                        name: "FK_Networks_Users_NetworkeeId",
                        column: x => x.NetworkeeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Networks_Users_NetworkerId",
                        column: x => x.NetworkerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Networks_NetworkeeId",
                table: "Networks",
                column: "NetworkeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Networks");
        }
    }
}
