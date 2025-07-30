using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IzinTakipProject.Migrations
{
    /// <inheritdoc />
    public partial class AddTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Izinler_Calisanlar_CalisanId",
                table: "Izinler");

            migrationBuilder.DropTable(
                name: "Calisanlar");

            migrationBuilder.DropIndex(
                name: "IX_Izinler_CalisanId",
                table: "Izinler");

            migrationBuilder.DropColumn(
                name: "CalisanId",
                table: "Izinler");

            migrationBuilder.AddColumn<string>(
                name: "KullaniciId",
                table: "Izinler",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Izinler_KullaniciId",
                table: "Izinler",
                column: "KullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Izinler_AspNetUsers_KullaniciId",
                table: "Izinler",
                column: "KullaniciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Izinler_AspNetUsers_KullaniciId",
                table: "Izinler");

            migrationBuilder.DropIndex(
                name: "IX_Izinler_KullaniciId",
                table: "Izinler");

            migrationBuilder.DropColumn(
                name: "KullaniciId",
                table: "Izinler");

            migrationBuilder.AddColumn<int>(
                name: "CalisanId",
                table: "Izinler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Calisanlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmanId = table.Column<int>(type: "int", nullable: false),
                    YoneticiId = table.Column<int>(type: "int", nullable: true),
                    AdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calisanlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calisanlar_Calisanlar_YoneticiId",
                        column: x => x.YoneticiId,
                        principalTable: "Calisanlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Calisanlar_Departmanlar_DepartmanId",
                        column: x => x.DepartmanId,
                        principalTable: "Departmanlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Izinler_CalisanId",
                table: "Izinler",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_DepartmanId",
                table: "Calisanlar",
                column: "DepartmanId");

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_YoneticiId",
                table: "Calisanlar",
                column: "YoneticiId");

            migrationBuilder.AddForeignKey(
                name: "FK_Izinler_Calisanlar_CalisanId",
                table: "Izinler",
                column: "CalisanId",
                principalTable: "Calisanlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
