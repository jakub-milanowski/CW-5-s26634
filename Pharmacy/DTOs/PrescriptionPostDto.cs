using System.ComponentModel.DataAnnotations;

namespace Pharmacy.DTOs;

public class PrescriptionPostDto
{
    [Required]
    public int DoctorId { get; set; }
    
    [Required]
    [MinLength(1)]
    [MaxLength(10)]
    public ICollection<PrescriptionPostMedicamentDto> Medicaments { get; set; }
    
    [Required]
    public PrescriptionPostPatientDto Patient { get; set; }
    
    [Required]
    public DateOnly DueDate { get; set; }
}

public class PrescriptionPostMedicamentDto
{
    [Required]
    public int IdMedicament { get; set; }
    public int? Dose { get; set; }
    [Required]
    [MaxLength(100)]
    public string Details { get; set; }
}

public class PrescriptionPostPatientDto
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    [Required]
    public DateOnly BirthDate { get; set; }
}