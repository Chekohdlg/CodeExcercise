using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeExercise.DataAcccess.Migrations
{
    public partial class HasData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category_Id",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgeRestriction", "Category_Id", "Company", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 1, 2, null, "Mattel", "A new toy from mattel", "https://http2.mlstatic.com/D_NQ_NP_799752-MLM44066560930_112020-O.webp", "rex", 255m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgeRestriction", "Category_Id", "Company", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 2, 5, null, "Disney", "A new toy from disney", "https://http2.mlstatic.com/D_NQ_NP_799752-MLM44066560930_112020-O.webp", "Princess", 300m });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Category_Id",
                table: "Products",
                column: "Category_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_Category_Id",
                table: "Products",
                column: "Category_Id",
                principalTable: "Categories",
                principalColumn: "Category_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_Category_Id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Category_Id",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Category_Id",
                table: "Products");
        }
    }
}
