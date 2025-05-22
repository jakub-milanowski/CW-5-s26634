using Microsoft.EntityFrameworkCore;
using Pharmacy.Models;

namespace Pharmacy.Data;

public class AppDbContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<MedicamentPrescription> MedicamentPrescriptions { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var doctor = new Doctor
        {
            Id = 1,
            FirstName = "DoctorName",
            LastName = "DoctorLastName",
            Email = "DoctorEmail",
        };
        var medicament = new Medicament
        {
            Id = 1,
            Name = "MedicamentName",
            Description = "MedicamentDescription",
            Type = "MedicamentType",
        };

        var patient = new Patient
        {
            Id = 1,
            FirstName = "PatientName",
            LastName = "PatientLastName",
            BirthDate = DateOnly.FromDateTime(DateTime.Now)
        };

        var prescription = new Prescription
        {
            Id = 1,
            DoctorId = 1,
            PatientId = 1,
            DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
            Date = DateOnly.FromDateTime(DateTime.Now)
        };

        var medicamentPrescription = new MedicamentPrescription
        {
            MedicamentId = 1,
            PrescriptionId = 1,
            Dose = 2,
            Details = "Details of medicament prescription"
        };
        
        modelBuilder.Entity<Doctor>().HasData(doctor);
        modelBuilder.Entity<Medicament>().HasData(medicament);
        modelBuilder.Entity<Patient>().HasData(patient);
        modelBuilder.Entity<Prescription>().HasData(prescription);
        modelBuilder.Entity<MedicamentPrescription>().HasData(medicamentPrescription);
    }
    
}