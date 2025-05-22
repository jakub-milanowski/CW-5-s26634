using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Models;

[Table("Patient")]
public class Patient
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    
    public virtual ICollection<Prescription> Prescriptions { get; set; }
}