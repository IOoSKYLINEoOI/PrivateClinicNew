namespace Clinic.DataAccess.Models;

public class ImageEntity
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;

    public UserEntity? User { get; set; }
}
