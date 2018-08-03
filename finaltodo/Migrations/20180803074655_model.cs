using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace finaltodo.Migrations
{
    public partial class model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "checklist",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    checkname = table.Column<string>(nullable: true),
                    Todoid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checklist", x => x.id);
                    table.ForeignKey(
                        name: "FK_checklist_Todo_Todoid",
                        column: x => x.Todoid,
                        principalTable: "Todo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "labels",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    labelname = table.Column<string>(nullable: true),
                    Todoid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_labels", x => x.id);
                    table.ForeignKey(
                        name: "FK_labels_Todo_Todoid",
                        column: x => x.Todoid,
                        principalTable: "Todo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_checklist_Todoid",
                table: "checklist",
                column: "Todoid");

            migrationBuilder.CreateIndex(
                name: "IX_labels_Todoid",
                table: "labels",
                column: "Todoid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "checklist");

            migrationBuilder.DropTable(
                name: "labels");
        }
    }
}
