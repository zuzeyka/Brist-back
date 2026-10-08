using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slush.Migrations
{
    /// <inheritdoc />
    public partial class AddFriendRequestStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // No DropForeignKey/DropIndex calls here for dbWalletTransactions'
            // transactionObj FKs/index — this database's table never actually had
            // the two FKs (predates migration tracking), and the index has already
            // been dropped by an earlier, partially-applied run of this migration.

            // No AddColumn here for dbUsers.backgroundImage — this database already
            // has it (added outside migration tracking, same as the FK situation
            // above); only dbFriends.status is genuinely new here.

            // Existing rows were created under the old instant-friend model (no
            // request/accept step), so they backfill as already Accepted rather
            // than Pending — only requests created from here on start Pending.
            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "dbFriends",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "dbFriends");

            migrationBuilder.CreateIndex(
                name: "IX_dbWalletTransactions_transactionObj",
                table: "dbWalletTransactions",
                column: "transactionObj");
        }
    }
}
