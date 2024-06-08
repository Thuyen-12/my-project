﻿// <auto-generated />
using System;
using ALR.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ALR.Data.Database.Migrations
{
    [DbContext(typeof(ALRDBContext))]
    [Migration("20240414164214_updateServiceLevel")]
    partial class updateServiceLevel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ALR.Domain.Entities.Entities.BillHistoryEntity", b =>
                {
                    b.Property<Guid>("billId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserEntityID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("billType")
                        .HasColumnType("int");

                    b.Property<float>("cost")
                        .HasColumnType("real");

                    b.Property<DateTime>("paymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("billId");

                    b.HasIndex("UserEntityID");

                    b.ToTable("BillHistory");
                });

            modelBuilder.Entity("ALR.Domain.Entities.Entities.BookingScheduleEntity", b =>
                {
                    b.Property<Guid>("scheduleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("bookingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("bookingStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("landlordId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("tenantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("scheduleID");

                    b.HasIndex("tenantId");

                    b.ToTable("BookingSchedule");
                });

            modelBuilder.Entity("ALR.Domain.Entities.Entities.BoxChatEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BoxChatName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BoxChatEntities");
                });

            modelBuilder.Entity("ALR.Domain.Entities.Entities.BoxChatUserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BoxChatId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BoxChatId");

                    b.HasIndex("UserId");

                    b.ToTable("BoxChatUserEntities");
                });

            modelBuilder.Entity("ALR.Domain.Entities.Entities.MessageEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BoxChatId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BoxChatId");

                    b.HasIndex("SenderId");

                    b.ToTable("MessageEntities");
                });

            modelBuilder.Entity("ALR.Domain.Entities.Entities.MotelAddress", b =>
                {
                    b.Property<Guid>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Commune")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoreDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressId");

                    b.ToTable("MotelAddresses");
                });

            modelBuilder.Entity("ALR.Domain.Entities.Entities.MotelEntity", b =>
                {
                    b.Property<Guid>("motelID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("motelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("motelID");

                    b.HasIndex("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("Motel");
                });

            modelBuilder.Entity("ALR.Domain.Entities.Entities.RequestEntity", b =>
                {
                    b.Property<Guid>("requestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("requestDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("requestDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("requestStatus")
                        .HasColumnType("int");

                    b.Property<int>("requestType")
                        .HasColumnType("int");

                    b.Property<Guid>("userID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("requestID");

                    b.HasIndex("userID");

                    b.ToTable("Request");
                });

            modelBuilder.Entity("ALR.Domain.Entities.Entities.RoomEntity", b =>
                {
                    b.Property<Guid>("roomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("availableSlot")
                        .HasColumnType("int");

                    b.Property<Guid>("motelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("roomDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("roomNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("roomPrice")
                        .HasColumnType("real");

                    b.Property<int>("roomStatus")
                        .HasColumnType("int");

                    b.HasKey("roomId");

                    b.HasIndex("motelId");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("ALR.Domain.Entities.Entities.ServiceEntity", b =>
                {
                    b.Property<Guid>("serviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("discout")
                        .HasColumnType("real");

                    b.Property<DateTime>("expiredDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isActived")
                        .HasColumnType("bit");

                    b.Property<float>("price")
                        .HasColumnType("real");

                    b.Property<int>("serviceLevel")
                        .HasColumnType("int");

                    b.Property<string>("serviceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("serviceId");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("ALR.Domain.Entities.Entities.ServicesPackageEntity", b =>
                {
                    b.Property<Guid>("servicePackageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("endDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("serviceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("startDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<Guid>("userId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("servicePackageId");

                    b.HasIndex("serviceId");

                    b.HasIndex("userId");

                    b.ToTable("ServicePackage");
                });

            modelBuilder.Entity("ALR.Domain.Entities.PostEntity", b =>
                {
                    b.Property<Guid>("postId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("active")
                        .HasColumnType("int");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("motelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("postLevel")
                        .HasColumnType("int");

                    b.Property<DateTime>("publicDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("roomPrice")
                        .HasColumnType("real");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("userId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("postId");

                    b.HasIndex("motelId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("ALR.Domain.Entities.ProfileEntity", b =>
                {
                    b.Property<Guid>("ProfileID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProfileID");

                    b.ToTable("Profile");
                });

            modelBuilder.Entity("ALR.Domain.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("UserEntityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProfileID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.Property<Guid?>("roomId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserEntityID");

                    b.HasIndex("ProfileID")
                        .IsUnique();

                    b.HasIndex("roomId");

                    b.ToTable("UserInfo");
                });

            modelBuilder.Entity("ALR.Domain.Entities.UserTokenEntity", b =>
                {
                    b.Property<string>("AccessToken")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpireDateAccessToken")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpireDateRefreshToken")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AccessToken");

                    b.HasIndex("UserID");

                    b.ToTable("UserToken");
                });

            modelBuilder.Entity("ALR.Domain.Entities.Entities.BillHistoryEntity", b =>
                {
                    b.HasOne("ALR.Domain.Entities.UserEntity", "UserEntity")
                        .WithMany()
                        .HasForeignKey("UserEntityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("ALR.Domain.Entities.Entities.BookingScheduleEntity", b =>
                {
                    b.HasOne("ALR.Domain.Entities.UserEntity", "tenant")
                        .WithMany()
                        .HasForeignKey("tenantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("tenant");
                });

            modelBuilder.Entity("ALR.Domain.Entities.Entities.BoxChatUserEntity", b =>
                {
                    b.HasOne("ALR.Domain.Entities.Entities.BoxChatEntity", "BoxChat")
                        .WithMany()
                        .HasForeignKey("BoxChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ALR.Domain.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BoxChat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ALR.Domain.Entities.Entities.MessageEntity", b =>
                {
                    b.HasOne("ALR.Domain.Entities.Entities.BoxChatEntity", "BoxChat")
                        .WithMany()
                        .HasForeignKey("BoxChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ALR.Domain.Entities.UserEntity", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BoxChat");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("ALR.Domain.Entities.Entities.MotelEntity", b =>
                {
                    b.HasOne("ALR.Domain.Entities.Entities.MotelAddress", "MotelAddress")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ALR.Domain.Entities.UserEntity", "Landlord")
                        .WithMany("Motels")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Landlord");

                    b.Navigation("MotelAddress");
                });

            modelBuilder.Entity("ALR.Domain.Entities.Entities.RequestEntity", b =>
                {
                    b.HasOne("ALR.Domain.Entities.UserEntity", "userEntity")
                        .WithMany("Request")
                        .HasForeignKey("userID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("userEntity");
                });

            modelBuilder.Entity("ALR.Domain.Entities.Entities.RoomEntity", b =>
                {
                    b.HasOne("ALR.Domain.Entities.Entities.MotelEntity", "Motel")
                        .WithMany()
                        .HasForeignKey("motelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Motel");
                });

            modelBuilder.Entity("ALR.Domain.Entities.Entities.ServicesPackageEntity", b =>
                {
                    b.HasOne("ALR.Domain.Entities.Entities.ServiceEntity", "Service")
                        .WithMany()
                        .HasForeignKey("serviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ALR.Domain.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ALR.Domain.Entities.PostEntity", b =>
                {
                    b.HasOne("ALR.Domain.Entities.Entities.MotelEntity", "motel")
                        .WithMany()
                        .HasForeignKey("motelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("motel");
                });

            modelBuilder.Entity("ALR.Domain.Entities.UserEntity", b =>
                {
                    b.HasOne("ALR.Domain.Entities.ProfileEntity", "Profile")
                        .WithOne("userEntity")
                        .HasForeignKey("ALR.Domain.Entities.UserEntity", "ProfileID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ALR.Domain.Entities.Entities.RoomEntity", "Room")
                        .WithMany()
                        .HasForeignKey("roomId");

                    b.Navigation("Profile");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("ALR.Domain.Entities.UserTokenEntity", b =>
                {
                    b.HasOne("ALR.Domain.Entities.UserEntity", "userEntity")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("userEntity");
                });

            modelBuilder.Entity("ALR.Domain.Entities.ProfileEntity", b =>
                {
                    b.Navigation("userEntity")
                        .IsRequired();
                });

            modelBuilder.Entity("ALR.Domain.Entities.UserEntity", b =>
                {
                    b.Navigation("Motels");

                    b.Navigation("Request");
                });
#pragma warning restore 612, 618
        }
    }
}
