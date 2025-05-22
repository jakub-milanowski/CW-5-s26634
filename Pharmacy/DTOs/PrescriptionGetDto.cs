namespace Pharmacy.DTOs;

public class PrescriptionGetDto
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
    public DoctorGetDto DoctorGet { get; set; }
    public ICollection<MedicamentGetDto> Medicaments { get; set; }
}