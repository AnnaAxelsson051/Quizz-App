using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorQuiz.Server.Migrations
{
    public partial class nullablemediaref : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionModels_MediaModels_MediaRefId",
                table: "QuestionModels");

            migrationBuilder.AlterColumn<int>(
                name: "MediaRefId",
                table: "QuestionModels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionModels_MediaModels_MediaRefId",
                table: "QuestionModels",
                column: "MediaRefId",
                principalTable: "MediaModels",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionModels_MediaModels_MediaRefId",
                table: "QuestionModels");

            migrationBuilder.AlterColumn<int>(
                name: "MediaRefId",
                table: "QuestionModels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionModels_MediaModels_MediaRefId",
                table: "QuestionModels",
                column: "MediaRefId",
                principalTable: "MediaModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
