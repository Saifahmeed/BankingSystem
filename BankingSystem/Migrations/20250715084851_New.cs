using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingSystem.Migrations
{
    /// <inheritdoc />
    public partial class New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountType",
                columns: table => new
                {
                    AccountType_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountType", x => x.AccountType_id);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Branch_id = table.Column<long>(type: "bigint", nullable: false),
                    Branch_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Location = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Branch_id);
                });

            migrationBuilder.CreateTable(
                name: "Look_AccountStatus",
                columns: table => new
                {
                    Status_id = table.Column<long>(type: "bigint", nullable: false),
                    Status_name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Look_AccountStatus", x => x.Status_id);
                });

            migrationBuilder.CreateTable(
                name: "Look_Currency",
                columns: table => new
                {
                    Currency_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ExchangeRate = table.Column<decimal>(type: "decimal(20,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Look_Currency", x => x.Currency_id);
                });

            migrationBuilder.CreateTable(
                name: "Look_UserType",
                columns: table => new
                {
                    TypeID = table.Column<int>(type: "int", nullable: false),
                    TypeName = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Look_UserType", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fname = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Lname = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    UserType_id = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_id);
                    table.ForeignKey(
                        name: "FK_Users_Look_UserType",
                        column: x => x.UserType_id,
                        principalTable: "Look_UserType",
                        principalColumn: "TypeID");
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Account_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<long>(type: "bigint", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AccountStatus_id = table.Column<long>(type: "bigint", nullable: true),
                    Branch_id = table.Column<long>(type: "bigint", nullable: true),
                    Date_opened = table.Column<DateTime>(type: "datetime", nullable: true),
                    Date_closed = table.Column<DateTime>(type: "datetime", nullable: true),
                    AccountType_id = table.Column<int>(type: "int", nullable: false),
                    Currency_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Account_ID);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountType",
                        column: x => x.AccountType_id,
                        principalTable: "AccountType",
                        principalColumn: "AccountType_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Branches",
                        column: x => x.Branch_id,
                        principalTable: "Branches",
                        principalColumn: "Branch_id");
                    table.ForeignKey(
                        name: "FK_Accounts_Currency",
                        column: x => x.Currency_id,
                        principalTable: "Look_Currency",
                        principalColumn: "Currency_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Look_AccountStatus",
                        column: x => x.AccountStatus_id,
                        principalTable: "Look_AccountStatus",
                        principalColumn: "Status_id");
                    table.ForeignKey(
                        name: "FK_Accounts_Users",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "User_id");
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Transaction_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<DateTime>(type: "datetime", nullable: true),
                    EquivalentRate = table.Column<decimal>(type: "decimal(20,4)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Sender_Account_id = table.Column<long>(type: "bigint", nullable: true),
                    Receiver_Account_id = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Transaction_id);
                    table.ForeignKey(
                        name: "FK_Transaction_Accounts",
                        column: x => x.Sender_Account_id,
                        principalTable: "Accounts",
                        principalColumn: "Account_ID");
                    table.ForeignKey(
                        name: "FK_Transaction_Accounts1",
                        column: x => x.Receiver_Account_id,
                        principalTable: "Accounts",
                        principalColumn: "Account_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountStatus_id",
                table: "Accounts",
                column: "AccountStatus_id");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountType_id",
                table: "Accounts",
                column: "AccountType_id");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Branch_id",
                table: "Accounts",
                column: "Branch_id");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Currency_id",
                table: "Accounts",
                column: "Currency_id");

            migrationBuilder.CreateIndex(
                name: "UX_User_AccountType",
                table: "Accounts",
                columns: new[] { "User_ID", "AccountType_id" },
                unique: true,
                filter: "[User_ID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Receiver_Account_id",
                table: "Transaction",
                column: "Receiver_Account_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Sender_Account_id",
                table: "Transaction",
                column: "Sender_Account_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserType_id",
                table: "Users",
                column: "UserType_id");

            migrationBuilder.CreateIndex(
                name: "UX_User_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_User_Phone",
                table: "Users",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountType");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Look_Currency");

            migrationBuilder.DropTable(
                name: "Look_AccountStatus");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Look_UserType");
        }
    }
}
