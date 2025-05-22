using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Migrations
{
    /// <inheritdoc />
    public partial class Adddefaultdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[] { 1, "DoctorEmail", "DoctorName", "DoctorLastName" });

            migrationBuilder.InsertData(
                table: "Medicament",
                columns: new[] { "Id", "Description", "Name", "Type" },
                values: new object[] { 1, "MedicamentDescription", "MedicamentName", "MedicamentType" });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "Id", "BirthDate", "FirstName", "LastName" },
                values: new object[] { 1, new DateOnly(2025, 5, 21), "PatientName", "PatientLastName" });

            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "Id", "Date", "DoctorId", "DueDate", "PatientId" },
                values: new object[] { 1, new DateOnly(2025, 5, 21), 1, new DateOnly(2025, 5, 28), 1 });

            migrationBuilder.InsertData(
                table: "MedicamentPrescription",
                columns: new[] { "MedicamentId", "PrescriptionId", "Details", "Dose" },
                values: new object[] { 1, 1, "Details of medicament prescription", 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MedicamentPrescription",
                keyColumns: new[] { "MedicamentId", "PrescriptionId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Medicament",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prescription",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
