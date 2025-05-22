namespace Pharmacy.DTOs;

public class PatientGetDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public ICollection<PrescriptionGetDto> Prescriptions { get; set; }
}

