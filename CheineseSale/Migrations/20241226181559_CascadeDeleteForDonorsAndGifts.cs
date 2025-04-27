using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CheineseSale.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeleteForDonorsAndGifts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catagory",
                columns: table => new
                {
                    Catagory_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Catagory_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catagory", x => x.Catagory_id);
                });

            migrationBuilder.CreateTable(
                name: "Donter",
                columns: table => new
                {
                    Donter_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Donter_first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Donter_Last_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Donter_Phon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Donter_Mail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donter", x => x.Donter_id);
                });

            migrationBuilder.CreateTable(
                name: "Gifts_Images",
                columns: table => new
                {
                    Image_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gifts_Images", x => x.Image_Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    User_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    User_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    User_Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserAdress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserRole = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.User_Id);
                });

            migrationBuilder.CreateTable(
                name: "Gift",
                columns: table => new
                {
                    Gift_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gift_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Catagory_id = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Donter_Id = table.Column<int>(type: "int", nullable: false),
                    Image_Id = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Purchaser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gift", x => x.Gift_Id);
                    table.ForeignKey(
                        name: "FK_Gift_Catagory",
                        column: x => x.Catagory_id,
                        principalTable: "Catagory",
                        principalColumn: "Catagory_id");
                    table.ForeignKey(
                        name: "FK_Gift_Donter",
                        column: x => x.Donter_Id,
                        principalTable: "Donter",
                        principalColumn: "Donter_id");
                    table.ForeignKey(
                        name: "FK_Gift_Gifts_Images",
                        column: x => x.Image_Id,
                        principalTable: "Gifts_Images",
                        principalColumn: "Image_Id");
                });

            migrationBuilder.CreateTable(
                name: "UserGift",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GiftId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_UserGift_Gift",
                        column: x => x.GiftId,
                        principalTable: "Gift",
                        principalColumn: "Gift_Id");
                });

            migrationBuilder.CreateTable(
                name: "Winner",
                columns: table => new
                {
                    Winner_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gift_Id = table.Column<int>(type: "int", nullable: false),
                    User_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Winner", x => x.Winner_id);
                    table.ForeignKey(
                        name: "FK_Winner_Gift",
                        column: x => x.Gift_Id,
                        principalTable: "Gift",
                        principalColumn: "Gift_Id");
                    table.ForeignKey(
                        name: "FK_Winner_User",
                        column: x => x.User_Id,
                        principalTable: "User",
                        principalColumn: "User_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gift_Catagory_id",
                table: "Gift",
                column: "Catagory_id");

            migrationBuilder.CreateIndex(
                name: "IX_Gift_Donter_Id",
                table: "Gift",
                column: "Donter_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Gift_Image_Id",
                table: "Gift",
                column: "Image_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserGift_GiftId",
                table: "UserGift",
                column: "GiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Winner_Gift_Id",
                table: "Winner",
                column: "Gift_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Winner_User_Id",
                table: "Winner",
                column: "User_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGift");

            migrationBuilder.DropTable(
                name: "Winner");

            migrationBuilder.DropTable(
                name: "Gift");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Catagory");

            migrationBuilder.DropTable(
                name: "Donter");

            migrationBuilder.DropTable(
                name: "Gifts_Images");
        }
    }
}
