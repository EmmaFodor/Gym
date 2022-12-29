using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proiect.Migrations
{
    public partial class Trainer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Trainer",
                table: "Gym");

            migrationBuilder.AddColumn<int>(
                name: "TrainerID",
                table: "Gym",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Trainer",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainer", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gym_TrainerID",
                table: "Gym",
                column: "TrainerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Gym_Trainer_TrainerID",
                table: "Gym",
                column: "TrainerID",
                principalTable: "Trainer",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gym_Trainer_TrainerID",
                table: "Gym");

            migrationBuilder.DropTable(
                name: "Trainer");

            migrationBuilder.DropIndex(
                name: "IX_Gym_TrainerID",
                table: "Gym");

            migrationBuilder.DropColumn(
                name: "TrainerID",
                table: "Gym");

            migrationBuilder.AddColumn<string>(
                name: "Trainer",
                table: "Gym",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
