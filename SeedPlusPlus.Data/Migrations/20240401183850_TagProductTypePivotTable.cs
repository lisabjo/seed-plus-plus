using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeedPlusPlus.Data.Migrations
{
    /// <inheritdoc />
    public partial class TagProductTypePivotTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_ProductType_ProductTypeId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_ProductTypeId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "Tag");

            migrationBuilder.CreateTable(
                name: "ProductTypeTag",
                columns: table => new
                {
                    ProductTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypeTag", x => new { x.ProductTypeId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ProductTypeTag_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTypeTag_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeTag_TagsId",
                table: "ProductTypeTag",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductTypeTag");

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "Tag",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_ProductTypeId",
                table: "Tag",
                column: "ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_ProductType_ProductTypeId",
                table: "Tag",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "Id");
        }
    }
}
