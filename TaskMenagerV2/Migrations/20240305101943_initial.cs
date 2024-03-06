using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskMenagerV2.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        //protected override void Up(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.AlterDatabase()
        //        .Annotation("MySql:CharSet", "utf8mb4");

        //    migrationBuilder.CreateTable(
        //        name: "Projects",
        //        columns: table => new
        //        {
        //            ProjectId = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        //            Name = table.Column<string>(type: "longtext", nullable: false)
        //                .Annotation("MySql:CharSet", "utf8mb4"),
        //            Description = table.Column<string>(type: "longtext", nullable: false)
        //                .Annotation("MySql:CharSet", "utf8mb4")
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_Projects", x => x.ProjectId);
        //        })
        //        .Annotation("MySql:CharSet", "utf8mb4");

        //    migrationBuilder.CreateTable(
        //        name: "Tasks",
        //            columns: table => new
        //            {
        //                Id = table.Column<int>(type: "int", nullable: false)
        //                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        //                ProjectId = table.Column<int>(type: "int", nullable: false),
        //                Description = table.Column<string>(type: "longtext", nullable: false)
        //                    .Annotation("MySql:CharSet", "utf8mb4"),
        //                DateOfCreation = table.Column<string>(type: "longtext", nullable: false)
        //                    .Annotation("MySql:CharSet", "utf8mb4"),
        //                CompletionDate = table.Column<string>(type: "longtext", nullable: false)
        //                    .Annotation("MySql:CharSet", "utf8mb4"),
        //            },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_Tasks", x => x.Id);
        //            table.ForeignKey(
        //                name: "FK_Tasks_Projects_ProjectId",
        //                column: x => x.ProjectId,
        //                principalTable: "Projects",
        //                principalColumn: "ProjectId",
        //                onDelete: ReferentialAction.Cascade);
        //        })
        //        .Annotation("MySql:CharSet", "utf8mb4");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Tasks_ProjectId",
        //        table: "Tasks",
        //        column: "ProjectId");
        //}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
