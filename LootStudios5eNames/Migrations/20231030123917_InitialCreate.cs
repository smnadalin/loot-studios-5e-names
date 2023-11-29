using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LootStudios5eNames.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Miniature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LS_Pack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LS_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LS_Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LS_Race = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LS_Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LS_Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LS_Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    _5e_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    _5e_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    _5e_Size = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Miniature", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Miniature");
        }
    }
}
