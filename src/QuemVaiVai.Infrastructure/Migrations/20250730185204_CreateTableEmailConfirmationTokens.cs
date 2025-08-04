using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QuemVaiVai.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableEmailConfirmationTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "updated_user",
                table: "votes");

            migrationBuilder.AddColumn<int>(
                name: "updated_user",
                table: "votes",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "deleted_user",
                table: "votes");

            migrationBuilder.AddColumn<int>(
                name: "deleted_user",
                table: "votes",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "created_user",
                table: "votes");

            migrationBuilder.AddColumn<int>(
                name: "created_user",
                table: "votes",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "updated_user",
                table: "vote_options");

            migrationBuilder.AddColumn<int>(
                name: "updated_user",
                table: "vote_options",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "deleted_user",
                table: "vote_options");

            migrationBuilder.AddColumn<int>(
                name: "deleted_user",
                table: "vote_options",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "created_user",
                table: "vote_options");

            migrationBuilder.AddColumn<int>(
                name: "created_user",
                table: "vote_options",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "updated_user",
                table: "users");

            migrationBuilder.AddColumn<int>(
                name: "updated_user",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "deleted_user",
                table: "users");

            migrationBuilder.AddColumn<int>(
                name: "deleted_user",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "created_user",
                table: "users");

            migrationBuilder.AddColumn<int>(
                name: "created_user",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.AddColumn<bool>(
                name: "confirmed",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.DropColumn(
                name: "updated_user",
                table: "user_events");

            migrationBuilder.AddColumn<int>(
                name: "updated_user",
                table: "user_events",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "deleted_user",
                table: "user_events");

            migrationBuilder.AddColumn<int>(
                name: "deleted_user",
                table: "user_events",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "created_user",
                table: "user_events");

            migrationBuilder.AddColumn<int>(
                name: "created_user",
                table: "user_events",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "updated_user",
                table: "task_lists");

            migrationBuilder.AddColumn<int>(
                name: "updated_user",
                table: "task_lists",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "deleted_user",
                table: "task_lists");

            migrationBuilder.AddColumn<int>(
                name: "deleted_user",
                table: "task_lists",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "created_user",
                table: "task_lists");

            migrationBuilder.AddColumn<int>(
                name: "created_user",
                table: "task_lists",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "updated_user",
                table: "task_items");

            migrationBuilder.AddColumn<int>(
                name: "updated_user",
                table: "task_items",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "deleted_user",
                table: "task_items");

            migrationBuilder.AddColumn<int>(
                name: "deleted_user",
                table: "task_items",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "created_user",
                table: "task_items");

            migrationBuilder.AddColumn<int>(
                name: "created_user",
                table: "task_items",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "updated_user",
                table: "groups");

            migrationBuilder.AddColumn<int>(
                name: "updated_user",
                table: "groups",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "deleted_user",
                table: "groups");

            migrationBuilder.AddColumn<int>(
                name: "deleted_user",
                table: "groups",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "created_user",
                table: "groups");

            migrationBuilder.AddColumn<int>(
                name: "created_user",
                table: "groups",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.AlterColumn<int>(
                name: "updated_user",
                table: "groups",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.DropColumn(
                name: "updated_user",
                table: "group_users");

            migrationBuilder.AddColumn<int>(
                name: "updated_user",
                table: "group_users",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "deleted_user",
                table: "group_users");

            migrationBuilder.AddColumn<int>(
                name: "deleted_user",
                table: "group_users",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "created_user",
                table: "group_users");

            migrationBuilder.AddColumn<int>(
                name: "created_user",
                table: "group_users",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "updated_user",
                table: "events");

            migrationBuilder.AddColumn<int>(
                name: "updated_user",
                table: "events",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "deleted_user",
                table: "events");

            migrationBuilder.AddColumn<int>(
                name: "deleted_user",
                table: "events",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "created_user",
                table: "events");

            migrationBuilder.AddColumn<int>(
                name: "created_user",
                table: "events",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "updated_user",
                table: "comments");

            migrationBuilder.AddColumn<int>(
                name: "updated_user",
                table: "comments",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "deleted_user",
                table: "comments");

            migrationBuilder.AddColumn<int>(
                name: "deleted_user",
                table: "comments",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.DropColumn(
                name: "created_user",
                table: "comments");

            migrationBuilder.AddColumn<int>(
                name: "created_user",
                table: "comments",
                type: "integer",
                nullable: false,
                defaultValue: null);

            migrationBuilder.CreateTable(
                name: "email_confirmation_tokens",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", maxLength: 100, nullable: false),
                    token = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    used = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_user = table.Column<int>(type: "integer", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_user = table.Column<int>(type: "integer", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_user = table.Column<int>(type: "integer", nullable: false),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_email_confirmation_tokens", x => x.id);
                    table.ForeignKey(
                        name: "FK_email_confirmation_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_email_confirmation_tokens_user_id",
                table: "email_confirmation_tokens",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "email_confirmation_tokens");

            migrationBuilder.DropColumn(
                name: "confirmed",
                table: "users");

            migrationBuilder.AlterColumn<Guid>(
                name: "updated_user",
                table: "votes",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "deleted_user",
                table: "votes",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "created_user",
                table: "votes",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "updated_user",
                table: "vote_options",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "deleted_user",
                table: "vote_options",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "created_user",
                table: "vote_options",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "updated_user",
                table: "users",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "deleted_user",
                table: "users",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "created_user",
                table: "users",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "updated_user",
                table: "user_events",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "deleted_user",
                table: "user_events",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "created_user",
                table: "user_events",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "updated_user",
                table: "task_lists",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "deleted_user",
                table: "task_lists",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "created_user",
                table: "task_lists",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "updated_user",
                table: "task_items",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "deleted_user",
                table: "task_items",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "created_user",
                table: "task_items",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "updated_user",
                table: "groups",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "deleted_user",
                table: "groups",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "created_user",
                table: "groups",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "updated_user",
                table: "group_users",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "deleted_user",
                table: "group_users",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "created_user",
                table: "group_users",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "updated_user",
                table: "events",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "deleted_user",
                table: "events",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "created_user",
                table: "events",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "updated_user",
                table: "comments",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "deleted_user",
                table: "comments",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "created_user",
                table: "comments",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
