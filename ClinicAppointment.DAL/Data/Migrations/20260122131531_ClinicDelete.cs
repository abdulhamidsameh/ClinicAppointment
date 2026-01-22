using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicAppointment.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class ClinicDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Clinics",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Clinics");
        }
    }
}
