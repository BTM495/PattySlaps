using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PattySlapsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddInventoryRecordsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemID",
                table: "InventoryRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryRecords_ItemID",
                table: "InventoryRecords",
                column: "ItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryRecords_Items_ItemID",
                table: "InventoryRecords",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryRecords_Items_ItemID",
                table: "InventoryRecords");

            migrationBuilder.DropIndex(
                name: "IX_InventoryRecords_ItemID",
                table: "InventoryRecords");

            migrationBuilder.DropColumn(
                name: "ItemID",
                table: "InventoryRecords");
        }
    }
}
