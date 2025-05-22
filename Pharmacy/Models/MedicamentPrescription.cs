using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pharmacy.Models;

[Table("MedicamentPrescription")]
[PrimaryKey(nameof(MedicamentId), nameof(PrescriptionId))]
public class MedicamentPrescription
{
    public int? Dose { get; set; }
    [MaxLength(100)]
    public string Details { get; set; }
    public int MedicamentId { get; set; }
    public int PrescriptionId { get; set; }
    
    [ForeignKey(nameof(PrescriptionId))]
    public virtual Prescription Prescription { get; set; }
    
    [ForeignKey(nameof(MedicamentId))]
    public virtual Medicament Medicament { get; set; }
}