﻿// <auto-generated />
using System;
using Energetic.NET.Basic.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Energetic.NET.Basic.Infrastructure.Migrations
{
    [DbContext(typeof(BasicDbContext))]
    [Migration("20240331081010_InitCreate")]
    partial class InitCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Energetic.NET.Basic.Domain.Models.FileAttachment", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("content_type");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedTime")
                        .HasPrecision(3)
                        .HasColumnType("datetime(3)")
                        .HasColumnName("created_time");

                    b.Property<long>("CreatedUserId")
                        .HasColumnType("bigint")
                        .HasColumnName("created_user_id");

                    b.Property<string>("HashCode")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(false)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("hash_code");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .IsUnicode(false)
                        .HasColumnType("varchar(128)")
                        .HasColumnName("name");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(512)
                        .IsUnicode(false)
                        .HasColumnType("varchar(512)")
                        .HasColumnName("path");

                    b.Property<long>("Size")
                        .HasColumnType("bigint")
                        .HasColumnName("size");

                    b.HasKey("Id")
                        .HasName("pk_sys_file_attachment");

                    b.ToTable("sys_file_attachment");
                });

            modelBuilder.Entity("Energetic.NET.Basic.Domain.Models.Resource", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("ApiUrl")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("api_url");

                    b.Property<string>("Code")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("code");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedTime")
                        .HasPrecision(3)
                        .HasColumnType("datetime(3)")
                        .HasColumnName("created_time");

                    b.Property<long>("CreatedUserId")
                        .HasColumnType("bigint")
                        .HasColumnName("created_user_id");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("deleted_by");

                    b.Property<DateTime?>("DeletedTime")
                        .HasPrecision(3)
                        .HasColumnType("datetime(3)")
                        .HasColumnName("deleted_time");

                    b.Property<long?>("DeletedUserId")
                        .HasColumnType("bigint")
                        .HasColumnName("deleted_user_id");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int")
                        .HasColumnName("display_order");

                    b.Property<string>("Icon")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("icon");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_deleted");

                    b.Property<bool>("IsEnable")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_enable");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("name");

                    b.Property<long>("ParentId")
                        .HasColumnType("bigint")
                        .HasColumnName("parent_id");

                    b.Property<string>("ReleationIds")
                        .IsRequired()
                        .HasMaxLength(128)
                        .IsUnicode(false)
                        .HasColumnType("varchar(128)")
                        .HasColumnName("releation_ids");

                    b.Property<string>("RequestMethod")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("request_method");

                    b.Property<string>("RoutePath")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("route_path");

                    b.Property<int>("Type")
                        .HasColumnType("int")
                        .HasColumnName("type");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("updated_by");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasPrecision(3)
                        .HasColumnType("datetime(3)")
                        .HasColumnName("updated_time");

                    b.Property<long?>("UpdatedUserId")
                        .HasColumnType("bigint")
                        .HasColumnName("updated_user_id");

                    b.HasKey("Id")
                        .HasName("pk_sys_resource");

                    b.ToTable("sys_resource");
                });

            modelBuilder.Entity("Energetic.NET.Basic.Domain.Models.Role", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("code");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedTime")
                        .HasPrecision(3)
                        .HasColumnType("datetime(3)")
                        .HasColumnName("created_time");

                    b.Property<long>("CreatedUserId")
                        .HasColumnType("bigint")
                        .HasColumnName("created_user_id");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("deleted_by");

                    b.Property<DateTime?>("DeletedTime")
                        .HasPrecision(3)
                        .HasColumnType("datetime(3)")
                        .HasColumnName("deleted_time");

                    b.Property<long?>("DeletedUserId")
                        .HasColumnType("bigint")
                        .HasColumnName("deleted_user_id");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .IsUnicode(false)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("description");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("name");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("updated_by");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasPrecision(3)
                        .HasColumnType("datetime(3)")
                        .HasColumnName("updated_time");

                    b.Property<long?>("UpdatedUserId")
                        .HasColumnType("bigint")
                        .HasColumnName("updated_user_id");

                    b.HasKey("Id")
                        .HasName("pk_sys_role");

                    b.ToTable("sys_role");
                });

            modelBuilder.Entity("Energetic.NET.Basic.Domain.Models.RoleResource", b =>
                {
                    b.Property<long>("ResourceId")
                        .HasColumnType("bigint")
                        .HasColumnName("resource_id");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint")
                        .HasColumnName("role_id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedTime")
                        .HasPrecision(3)
                        .HasColumnType("datetime(3)")
                        .HasColumnName("created_time");

                    b.Property<long>("CreatedUserId")
                        .HasColumnType("bigint")
                        .HasColumnName("created_user_id");

                    b.HasKey("ResourceId", "RoleId")
                        .HasName("pk_sys_role_resource");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_sys_role_resource_role_id");

                    b.ToTable("sys_role_resource");
                });

            modelBuilder.Entity("Energetic.NET.Basic.Domain.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long?>("AvatarId")
                        .HasColumnType("bigint")
                        .HasColumnName("avatar_id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedTime")
                        .HasPrecision(3)
                        .HasColumnType("datetime(3)")
                        .HasColumnName("created_time");

                    b.Property<long>("CreatedUserId")
                        .HasColumnType("bigint")
                        .HasColumnName("created_user_id");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("deleted_by");

                    b.Property<DateTime?>("DeletedTime")
                        .HasPrecision(3)
                        .HasColumnType("datetime(3)")
                        .HasColumnName("deleted_time");

                    b.Property<long?>("DeletedUserId")
                        .HasColumnType("bigint")
                        .HasColumnName("deleted_user_id");

                    b.Property<string>("EmailAddress")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("email_address");

                    b.Property<int>("Gender")
                        .HasColumnType("int")
                        .HasColumnName("gender");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_admin");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_deleted");

                    b.Property<bool>("IsEnable")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_enable");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("nick_name");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(11)
                        .IsUnicode(false)
                        .HasColumnType("char(11)")
                        .HasColumnName("phone_number")
                        .IsFixedLength();

                    b.Property<string>("RealName")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("real_name");

                    b.Property<int>("RegisterWay")
                        .HasColumnType("int")
                        .HasColumnName("register_way");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("updated_by");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasPrecision(3)
                        .HasColumnType("datetime(3)")
                        .HasColumnName("updated_time");

                    b.Property<long?>("UpdatedUserId")
                        .HasColumnType("bigint")
                        .HasColumnName("updated_user_id");

                    b.Property<string>("UserName")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("user_name");

                    b.Property<string>("passwordHash")
                        .HasMaxLength(32)
                        .IsUnicode(true)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("password_hash");

                    b.HasKey("Id")
                        .HasName("pk_sys_user");

                    b.ToTable("sys_user");
                });

            modelBuilder.Entity("Energetic.NET.Basic.Domain.Models.UserLoginHistory", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Browser")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("browser");

                    b.Property<DateTime>("CreatedTime")
                        .HasPrecision(3)
                        .HasColumnType("datetime(3)")
                        .HasColumnName("created_time");

                    b.Property<string>("LoginAccount")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("login_account");

                    b.Property<string>("LoginIp")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("login_ip");

                    b.Property<string>("LoginLocation")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("login_location");

                    b.Property<int>("LoginResult")
                        .HasColumnType("int")
                        .HasColumnName("login_result");

                    b.Property<int>("LoginWay")
                        .HasColumnType("int")
                        .HasColumnName("login_way");

                    b.Property<string>("Message")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("message");

                    b.Property<string>("OperatingSystem")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("operating_system");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_sys_user_login_history");

                    b.ToTable("sys_user_login_history");
                });

            modelBuilder.Entity("Energetic.NET.Basic.Domain.Models.UserOperationHistory", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedTime")
                        .HasPrecision(3)
                        .HasColumnType("datetime(3)")
                        .HasColumnName("created_time");

                    b.Property<string>("FunctionName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("function_name");

                    b.Property<string>("Message")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("message");

                    b.Property<string>("ModuleName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("module_name");

                    b.Property<string>("NickName")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("nick_name");

                    b.Property<string>("OperatorAccount")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("operator_account");

                    b.Property<string>("RequestAddress")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("request_address");

                    b.Property<string>("RequestMethod")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("request_method");

                    b.Property<int>("RequestResult")
                        .HasColumnType("int")
                        .HasColumnName("request_result");

                    b.Property<int>("TakingTime")
                        .HasColumnType("int")
                        .HasColumnName("taking_time");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_sys_user_operation_history");

                    b.ToTable("sys_user_operation_history");
                });

            modelBuilder.Entity("Energetic.NET.Basic.Domain.Models.UserRole", b =>
                {
                    b.Property<long>("RoleId")
                        .HasColumnType("bigint")
                        .HasColumnName("role_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedTime")
                        .HasPrecision(3)
                        .HasColumnType("datetime(3)")
                        .HasColumnName("created_time");

                    b.Property<long>("CreatedUserId")
                        .HasColumnType("bigint")
                        .HasColumnName("created_user_id");

                    b.HasKey("RoleId", "UserId")
                        .HasName("pk_sys_user_role");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_sys_user_role_user_id");

                    b.ToTable("sys_user_role");
                });

            modelBuilder.Entity("Energetic.NET.Basic.Domain.Models.RoleResource", b =>
                {
                    b.HasOne("Energetic.NET.Basic.Domain.Models.Resource", null)
                        .WithMany()
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_sys_role_resource_sys_resource_resource_id");

                    b.HasOne("Energetic.NET.Basic.Domain.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_sys_role_resource_sys_role_role_id");
                });

            modelBuilder.Entity("Energetic.NET.Basic.Domain.Models.UserRole", b =>
                {
                    b.HasOne("Energetic.NET.Basic.Domain.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_sys_user_role_sys_role_role_id");

                    b.HasOne("Energetic.NET.Basic.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_sys_user_role_sys_user_user_id");
                });
#pragma warning restore 612, 618
        }
    }
}
