﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatingApp.API.Data.Migrations
{
    public partial class AddPasswordToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "AppUsers",
                type: "longblob",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "AppUsers",
                type: "longblob",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "AppUsers");
        }
    }
}
