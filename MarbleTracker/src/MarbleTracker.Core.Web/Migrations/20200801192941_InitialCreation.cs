using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarbleTracker.Core.Web.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    Principal = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    Principal = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    MarbleAmount = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Challenges",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    Principal = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    SourceGroupId = table.Column<long>(nullable: false),
                    TargetGroupId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Challenges_Groups_SourceGroupId",
                        column: x => x.SourceGroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Challenges_Groups_TargetGroupId",
                        column: x => x.TargetGroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChallengeResults",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    Principal = table.Column<string>(nullable: true),
                    Result = table.Column<int>(nullable: false),
                    WitnessId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChallengeResults_Users_WitnessId",
                        column: x => x.WitnessId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroupRelationships",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    Principal = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    GroupId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroupRelationships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGroupRelationships_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroupRelationships_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    Principal = table.Column<string>(nullable: true),
                    TransferAmount = table.Column<decimal>(nullable: false),
                    SenderBalance = table.Column<decimal>(nullable: false),
                    RecieverBalance = table.Column<decimal>(nullable: false),
                    SenderId = table.Column<long>(nullable: false),
                    ReceiverId = table.Column<long>(nullable: false),
                    RecieverId = table.Column<long>(nullable: true),
                    ChallengeResultId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_ChallengeResults_ChallengeResultId",
                        column: x => x.ChallengeResultId,
                        principalTable: "ChallengeResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_RecieverId",
                        column: x => x.RecieverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeResults_WitnessId",
                table: "ChallengeResults",
                column: "WitnessId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_SourceGroupId",
                table: "Challenges",
                column: "SourceGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_TargetGroupId",
                table: "Challenges",
                column: "TargetGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ChallengeResultId",
                table: "Transactions",
                column: "ChallengeResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_RecieverId",
                table: "Transactions",
                column: "RecieverId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SenderId",
                table: "Transactions",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupRelationships_GroupId",
                table: "UserGroupRelationships",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupRelationships_UserId",
                table: "UserGroupRelationships",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Challenges");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UserGroupRelationships");

            migrationBuilder.DropTable(
                name: "ChallengeResults");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
