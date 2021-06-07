using Microsoft.EntityFrameworkCore.Migrations;

namespace MessageBoardApi.Migrations
{
    public partial class AddVirtalGroupMessageProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GroupMessage_GroupId",
                table: "GroupMessage",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessage_MessageId",
                table: "GroupMessage",
                column: "MessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMessage_Groups_GroupId",
                table: "GroupMessage",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMessage_Messages_MessageId",
                table: "GroupMessage",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "MessageId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupMessage_Groups_GroupId",
                table: "GroupMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMessage_Messages_MessageId",
                table: "GroupMessage");

            migrationBuilder.DropIndex(
                name: "IX_GroupMessage_GroupId",
                table: "GroupMessage");

            migrationBuilder.DropIndex(
                name: "IX_GroupMessage_MessageId",
                table: "GroupMessage");
        }
    }
}
