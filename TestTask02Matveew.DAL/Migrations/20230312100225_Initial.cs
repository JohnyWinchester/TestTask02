using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask02Matveew.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coordinates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoordinateX = table.Column<int>(type: "int", nullable: false),
                    CoordinateY = table.Column<int>(type: "int", nullable: false),
                    Client = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinates", x => x.Id);
                    table.CheckConstraint("Client", "Client = 'X' OR Client = '0'");
                    table.CheckConstraint("CoordinateX", "CoordinateX > 0 AND CoordinateX < 4");
                    table.CheckConstraint("CoordinateY", "CoordinateY > 0 AND CoordinateY < 4");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coordinates");
        }
    }
}
