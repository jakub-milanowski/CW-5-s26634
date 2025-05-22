using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Models;

[Table("Prescription")]
public class Prescription
{
    [Key]
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    
    [ForeignKey(nameof(DoctorId))]
    public virtual Doctor Doctor { get; set; }
    
    [ForeignKey(nameof(PatientId))]
    public virtual Patient Patient { get; set; }
    
    public virtual ICollection<MedicamentPrescription> MedicamentPrescriptions { get; set; }
}