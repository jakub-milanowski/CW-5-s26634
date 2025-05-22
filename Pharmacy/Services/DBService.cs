using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;
using Pharmacy.DTOs;
using Pharmacy.Exceptions;
using Pharmacy.Models;

namespace Pharmacy.Services;

public interface IDBService
{
    public Task<PatientGetDto?> GetPatientAsync(int id);
    public Task<PrescriptionGetDto?> CreatePrescriptionAsync(PrescriptionPostDto data);
}

public class DBService(AppDbContext data) : IDBService
{
    public async Task<PatientGetDto?> GetPatientAsync(int id)
    {
        Patient? patient = await data.Patients
            .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.Doctor)
            .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.MedicamentPrescriptions)
                .ThenInclude(mp => mp.Medicament)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (patient == null)
            return null;

        return new PatientGetDto()
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.BirthDate,
            Prescriptions = patient.Prescriptions.OrderBy(p => p.DueDate).Select(p => new PrescriptionGetDto()
            {
                Id = p.Id,
                Date = p.Date,
                DueDate = p.DueDate,
                DoctorGet = new DoctorGetDto()
                {
                    Id = p.Doctor.Id,
                    FirstName = p.Doctor.FirstName,
                    LastName = p.Doctor.LastName,
                    Email = p.Doctor.Email,
                },
                Medicaments = p.MedicamentPrescriptions.Select(
                    m => new MedicamentGetDto()
                    {
                        Id = m.Medicament.Id,
                        Name = m.Medicament.Name,
                        Dose = m.Dose,
                        Description = m.Medicament.Description,
                        Type = m.Medicament.Type
                    }).ToList()
            }).ToList(),
        };
    }

    public async Task<PrescriptionGetDto> CreatePrescriptionAsync(PrescriptionPostDto postData)
    {
        if (postData.DueDate < DateOnly.FromDateTime(DateTime.Now))
        {
            throw new InvalidArgumentException("Due date cannot be in the past.");
        }
        var medicaments = new List<Medicament>();
        foreach (var medicamentData in postData.Medicaments)
        {
            var medicament = await data.Medicaments.FirstOrDefaultAsync(m => m.Id == medicamentData.IdMedicament);
            if (medicament == null)
            {
                throw new NotFoundException($"Medicament with id {medicamentData.IdMedicament} not found");
            }
            medicaments.Add(medicament);
        }
        
        var doctor =await data.Doctors.FirstOrDefaultAsync(doctor => doctor.Id == postData.DoctorId);
        
        if (doctor == null)
        {
            throw new NotFoundException($"Doctor with id {postData.DoctorId} not found");
        }

        var existingPatient = await data.Patients.FirstOrDefaultAsync(p => 
            p.FirstName == postData.Patient.FirstName &&
            p.LastName == postData.Patient.LastName &&
            p.BirthDate == postData.Patient.BirthDate);

        Patient patient;
        if (existingPatient != null)
        {
            patient = existingPatient;
        }
        else
        {
            patient = new Patient()
            {
                FirstName = postData.Patient.FirstName,
                LastName = postData.Patient.LastName,
                BirthDate = postData.Patient.BirthDate,
            };
            await data.Patients.AddAsync(patient);
            await data.SaveChangesAsync();
        }
        
        var prescription = new Prescription()
        {
            DoctorId = postData.DoctorId,
            PatientId = patient.Id,
            Date = DateOnly.FromDateTime(DateTime.Now),
            DueDate = postData.DueDate,
            MedicamentPrescriptions = postData.Medicaments.Select(m => new MedicamentPrescription()
            {
                MedicamentId = m.IdMedicament,
                Details = m.Details,
                Dose = m.Dose
            }).ToList()
        };

        await data.Prescriptions.AddAsync(prescription);
        await data.SaveChangesAsync();
        return new PrescriptionGetDto()
        {
            Id = prescription.Id,
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            Medicaments = prescription.MedicamentPrescriptions.Select(m => new MedicamentGetDto()
            {
                Id = m.Medicament.Id,
                Name = m.Medicament.Name,
                Dose = m.Dose,
                Description = m.Medicament.Description,
                Type = m.Medicament.Type
            }).ToList()
        };
    }
}