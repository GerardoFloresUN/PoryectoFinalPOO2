using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dermatologia.Migrations
{
    /// <inheritdoc />
    public partial class RelacionDoctorCita : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DoctorId",
                table: "Citas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Citas_DoctorId",
                table: "Citas",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Doctores_DoctorId",
                table: "Citas",
                column: "DoctorId",
                principalTable: "Doctores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Doctores_DoctorId",
                table: "Citas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_DoctorId",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Citas");
        }
    }
}
