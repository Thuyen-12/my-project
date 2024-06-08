using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ALR.Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class GenDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoxChatEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoxChatName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxChatEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    postId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    roomPrice = table.Column<float>(type: "real", nullable: false),
                    publicDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    active = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.postId);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    ProfileID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.ProfileID);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    serviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    serviceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    expiredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    discout = table.Column<float>(type: "real", nullable: false),
                    isActived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.serviceId);
                });

            migrationBuilder.CreateTable(
                name: "BillHistory",
                columns: table => new
                {
                    billId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserEntityID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    billType = table.Column<int>(type: "int", nullable: false),
                    paymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cost = table.Column<float>(type: "real", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillHistory", x => x.billId);
                });

            migrationBuilder.CreateTable(
                name: "BookingSchedule",
                columns: table => new
                {
                    scheduleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    landlordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    bookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    bookingStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingSchedule", x => x.scheduleID);
                });

            migrationBuilder.CreateTable(
                name: "BoxChatUserEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoxChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxChatUserEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoxChatUserEntities_BoxChatEntities_BoxChatId",
                        column: x => x.BoxChatId,
                        principalTable: "BoxChatEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoxChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageEntities_BoxChatEntities_BoxChatId",
                        column: x => x.BoxChatId,
                        principalTable: "BoxChatEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Motel",
                columns: table => new
                {
                    motelID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    landlordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    motelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    motelAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motel", x => x.motelID);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    roomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    motelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    roomNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    roomDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    roomPrice = table.Column<float>(type: "real", nullable: false),
                    availableSlot = table.Column<int>(type: "int", nullable: false),
                    roomStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.roomId);
                    table.ForeignKey(
                        name: "FK_Room_Motel_motelId",
                        column: x => x.motelId,
                        principalTable: "Motel",
                        principalColumn: "motelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    UserEntityID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Account = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRole = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ProfileID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    roomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.UserEntityID);
                    table.ForeignKey(
                        name: "FK_UserInfo_Profile_ProfileID",
                        column: x => x.ProfileID,
                        principalTable: "Profile",
                        principalColumn: "ProfileID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInfo_Room_roomId",
                        column: x => x.roomId,
                        principalTable: "Room",
                        principalColumn: "roomId");
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    requestID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    requestType = table.Column<int>(type: "int", nullable: false),
                    requestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    requestDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    requestStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.requestID);
                    table.ForeignKey(
                        name: "FK_Request_UserInfo_userID",
                        column: x => x.userID,
                        principalTable: "UserInfo",
                        principalColumn: "UserEntityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicePackage",
                columns: table => new
                {
                    servicePackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    serviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePackage", x => x.servicePackageId);
                    table.ForeignKey(
                        name: "FK_ServicePackage_Service_serviceId",
                        column: x => x.serviceId,
                        principalTable: "Service",
                        principalColumn: "serviceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicePackage_UserInfo_userId",
                        column: x => x.userId,
                        principalTable: "UserInfo",
                        principalColumn: "UserEntityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    AccessToken = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpireDateAccessToken = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ExpireDateRefreshToken = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => x.AccessToken);
                    table.ForeignKey(
                        name: "FK_UserToken_UserInfo_UserID",
                        column: x => x.UserID,
                        principalTable: "UserInfo",
                        principalColumn: "UserEntityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillHistory_UserEntityID",
                table: "BillHistory",
                column: "UserEntityID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingSchedule_tenantId",
                table: "BookingSchedule",
                column: "tenantId");

            migrationBuilder.CreateIndex(
                name: "IX_BoxChatUserEntities_BoxChatId",
                table: "BoxChatUserEntities",
                column: "BoxChatId");

            migrationBuilder.CreateIndex(
                name: "IX_BoxChatUserEntities_UserId",
                table: "BoxChatUserEntities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageEntities_BoxChatId",
                table: "MessageEntities",
                column: "BoxChatId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageEntities_SenderId",
                table: "MessageEntities",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Motel_landlordId",
                table: "Motel",
                column: "landlordId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_userID",
                table: "Request",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_Room_motelId",
                table: "Room",
                column: "motelId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePackage_serviceId",
                table: "ServicePackage",
                column: "serviceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePackage_userId",
                table: "ServicePackage",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_ProfileID",
                table: "UserInfo",
                column: "ProfileID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_roomId",
                table: "UserInfo",
                column: "roomId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_UserID",
                table: "UserToken",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_BillHistory_UserInfo_UserEntityID",
                table: "BillHistory",
                column: "UserEntityID",
                principalTable: "UserInfo",
                principalColumn: "UserEntityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingSchedule_UserInfo_tenantId",
                table: "BookingSchedule",
                column: "tenantId",
                principalTable: "UserInfo",
                principalColumn: "UserEntityID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BoxChatUserEntities_UserInfo_UserId",
                table: "BoxChatUserEntities",
                column: "UserId",
                principalTable: "UserInfo",
                principalColumn: "UserEntityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageEntities_UserInfo_SenderId",
                table: "MessageEntities",
                column: "SenderId",
                principalTable: "UserInfo",
                principalColumn: "UserEntityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Motel_UserInfo_landlordId",
                table: "Motel",
                column: "landlordId",
                principalTable: "UserInfo",
                principalColumn: "UserEntityID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motel_UserInfo_landlordId",
                table: "Motel");

            migrationBuilder.DropTable(
                name: "BillHistory");

            migrationBuilder.DropTable(
                name: "BookingSchedule");

            migrationBuilder.DropTable(
                name: "BoxChatUserEntities");

            migrationBuilder.DropTable(
                name: "MessageEntities");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "ServicePackage");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "BoxChatEntities");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Motel");
        }
    }
}
