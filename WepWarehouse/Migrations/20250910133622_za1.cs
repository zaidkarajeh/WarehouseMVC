using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WepWarehouse.Migrations
{
    /// <inheritdoc />
    public partial class za1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "rolsId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoleModel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_rolsId",
                table: "AspNetUsers",
                column: "rolsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_RoleModel_rolsId",
                table: "AspNetUsers",
                column: "rolsId",
                principalTable: "RoleModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_RoleModel_rolsId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "RoleModel");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_rolsId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "rolsId",
                table: "AspNetUsers");
        }
    }
}
