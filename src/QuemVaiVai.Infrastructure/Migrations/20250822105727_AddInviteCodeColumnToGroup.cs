using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuemVaiVai.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInviteCodeColumnToGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "invite_code",
                table: "groups",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.NewGuid());
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "invite_code",
                table: "groups");
        }
    }
}
