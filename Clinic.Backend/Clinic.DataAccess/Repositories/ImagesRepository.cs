using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Models;
using Clinic.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic.DataAccess.Repositories;

public class ImagesRepository : IImagesRepository
{
    private readonly ClinicDbContext _context;

    public ImagesRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task Add(Image image)
    {
        var imageEntity = new ImageEntity()
        {
            Id = image.Id,
            FileName = image.FileName,
            FilePath = image.FilePath
        };

        await _context.Images.AddAsync(imageEntity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        await _context.Images
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<Image> GetById(Guid id)
    {
        var imageEntity = await _context.Images
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id)
            ?? throw new Exception($"Image with ID {id} not found.");

        var image = Image.Create(imageEntity.Id, imageEntity.FileName, imageEntity.FilePath).Value;

        return image;
    }
}