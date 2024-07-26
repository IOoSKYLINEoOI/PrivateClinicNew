using CSharpFunctionalExtensions;

namespace Clinic.Core.Models;

public class Image
{
    public const int MaxFileNameLength = 250;

    private Image(Guid id, string fileName, string filePath)
    {
        Id = id;
        FileName = fileName;
        FilePath = filePath;
    }

    public Guid Id { get; }
    public string FileName { get; }
    public string FilePath { get; }

    public static Result<Image> Create(Guid id, string fileName, string filePath)
    {
        if (string.IsNullOrEmpty(fileName) || fileName.Length > MaxFileNameLength)
        {
            return Result.Failure<Image>($"'{nameof(fileName)}' cannot be null, empty or more than {MaxFileNameLength} characters.");
        }

        var image = new Image(id, fileName, filePath);

        return Result.Success(image);
    }
}