﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Energetic.NET.Basic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_file_attachment",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hash_code = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    path = table.Column<string>(type: "varchar(512)", unicode: false, maxLength: 512, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    content_type = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    size = table.Column<long>(type: "bigint", nullable: false),
                    created_user_id = table.Column<long>(type: "bigint", nullable: false),
                    created_by = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_time = table.Column<DateTime>(type: "datetime(3)", precision: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_file_attachment", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_resource",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    parent_id = table.Column<long>(type: "bigint", nullable: false),
                    releation_ids = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    route_path = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    icon = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    display_order = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    is_enable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    api_url = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    request_method = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_user_id = table.Column<long>(type: "bigint", nullable: false),
                    created_by = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_time = table.Column<DateTime>(type: "datetime(3)", precision: 3, nullable: false),
                    updated_user_id = table.Column<long>(type: "bigint", nullable: true),
                    updated_by = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_time = table.Column<DateTime>(type: "datetime(3)", precision: 3, nullable: true),
                    deleted_user_id = table.Column<long>(type: "bigint", nullable: true),
                    deleted_by = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deleted_time = table.Column<DateTime>(type: "datetime(3)", precision: 3, nullable: true),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_resource", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_role",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_user_id = table.Column<long>(type: "bigint", nullable: false),
                    created_by = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_time = table.Column<DateTime>(type: "datetime(3)", precision: 3, nullable: false),
                    updated_user_id = table.Column<long>(type: "bigint", nullable: true),
                    updated_by = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_time = table.Column<DateTime>(type: "datetime(3)", precision: 3, nullable: true),
                    deleted_user_id = table.Column<long>(type: "bigint", nullable: true),
                    deleted_by = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deleted_time = table.Column<DateTime>(type: "datetime(3)", precision: 3, nullable: true),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_role", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    user_name = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nick_name = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    real_name = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone_number = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email_address = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gender = table.Column<int>(type: "int", nullable: false),
                    avatar_id = table.Column<long>(type: "bigint", nullable: true),
                    is_admin = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    register_way = table.Column<int>(type: "int", nullable: false),
                    is_enable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    password_hash = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_user_id = table.Column<long>(type: "bigint", nullable: false),
                    created_by = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_time = table.Column<DateTime>(type: "datetime(3)", precision: 3, nullable: false),
                    updated_user_id = table.Column<long>(type: "bigint", nullable: true),
                    updated_by = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_time = table.Column<DateTime>(type: "datetime(3)", precision: 3, nullable: true),
                    deleted_user_id = table.Column<long>(type: "bigint", nullable: true),
                    deleted_by = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deleted_time = table.Column<DateTime>(type: "datetime(3)", precision: 3, nullable: true),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_user", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_user_login_history",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    login_account = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    login_ip = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    login_location = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    browser = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    operating_system = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    login_way = table.Column<int>(type: "int", nullable: false),
                    login_result = table.Column<int>(type: "int", nullable: false),
                    message = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_time = table.Column<DateTime>(type: "datetime(3)", precision: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_user_login_history", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_user_operation_history",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    operator_account = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nick_name = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    module_name = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    function_name = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    request_address = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    request_method = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    request_result = table.Column<int>(type: "int", nullable: false),
                    taking_time = table.Column<int>(type: "int", nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime(3)", precision: 3, nullable: false),
                    message = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_user_operation_history", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_role_resource",
                columns: table => new
                {
                    role_id = table.Column<long>(type: "bigint", nullable: false),
                    resource_id = table.Column<long>(type: "bigint", nullable: false),
                    created_user_id = table.Column<long>(type: "bigint", nullable: false),
                    created_by = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_time = table.Column<DateTime>(type: "datetime(3)", precision: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_role_resource", x => new { x.resource_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_sys_role_resource_sys_resource_resource_id",
                        column: x => x.resource_id,
                        principalTable: "sys_resource",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sys_role_resource_sys_role_role_id",
                        column: x => x.role_id,
                        principalTable: "sys_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_user_role",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    role_id = table.Column<long>(type: "bigint", nullable: false),
                    created_user_id = table.Column<long>(type: "bigint", nullable: false),
                    created_by = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_time = table.Column<DateTime>(type: "datetime(3)", precision: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_user_role", x => new { x.role_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_sys_user_role_sys_role_role_id",
                        column: x => x.role_id,
                        principalTable: "sys_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sys_user_role_sys_user_user_id",
                        column: x => x.user_id,
                        principalTable: "sys_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_sys_role_resource_role_id",
                table: "sys_role_resource",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_sys_user_role_user_id",
                table: "sys_user_role",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_file_attachment");

            migrationBuilder.DropTable(
                name: "sys_role_resource");

            migrationBuilder.DropTable(
                name: "sys_user_login_history");

            migrationBuilder.DropTable(
                name: "sys_user_operation_history");

            migrationBuilder.DropTable(
                name: "sys_user_role");

            migrationBuilder.DropTable(
                name: "sys_resource");

            migrationBuilder.DropTable(
                name: "sys_role");

            migrationBuilder.DropTable(
                name: "sys_user");
        }
    }
}
