using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_EXAMEN_APP.Migrations
{
    /// <inheritdoc />
    public partial class final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_TypeBooks_TypeId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscribes_Subtypes_TypeID",
                table: "Subscribes");

            migrationBuilder.DropIndex(
                name: "IX_Subscribes_TypeID",
                table: "Subscribes");

            migrationBuilder.DropIndex(
                name: "IX_Books_TypeId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "TypeBooks");

            migrationBuilder.DropColumn(
                name: "Publishing_House_Id",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "SubTypeId",
                table: "Subscribes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypebookTypeId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribes_SubTypeId",
                table: "Subscribes",
                column: "SubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_TypebookTypeId",
                table: "Books",
                column: "TypebookTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_TypeBooks_TypebookTypeId",
                table: "Books",
                column: "TypebookTypeId",
                principalTable: "TypeBooks",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscribes_Subtypes_SubTypeId",
                table: "Subscribes",
                column: "SubTypeId",
                principalTable: "Subtypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_TypeBooks_TypebookTypeId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscribes_Subtypes_SubTypeId",
                table: "Subscribes");

            migrationBuilder.DropIndex(
                name: "IX_Subscribes_SubTypeId",
                table: "Subscribes");

            migrationBuilder.DropIndex(
                name: "IX_Books_TypebookTypeId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "SubTypeId",
                table: "Subscribes");

            migrationBuilder.DropColumn(
                name: "TypebookTypeId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "image",
                table: "Authors");

            migrationBuilder.AddColumn<string>(
                name: "Length",
                table: "TypeBooks",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Publishing_House_Id",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribes_TypeID",
                table: "Subscribes",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_TypeId",
                table: "Books",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_TypeBooks_TypeId",
                table: "Books",
                column: "TypeId",
                principalTable: "TypeBooks",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscribes_Subtypes_TypeID",
                table: "Subscribes",
                column: "TypeID",
                principalTable: "Subtypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
