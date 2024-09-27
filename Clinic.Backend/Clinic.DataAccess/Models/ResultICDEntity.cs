namespace Clinic.DataAccess.Models;

public class ResultICDEntity
{
    public Guid Id { get; set; }
    public string ICDCode { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid ReceptionId { get; set; }

    public ReceptionEntity? Reception { get; set; }
}
