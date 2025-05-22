namespace Pharmacy.DTOs;

public class MedicamentGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public int? Dose { get; set; }
}