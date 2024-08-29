using Clinic.Core.Models;

namespace Clinic.Core.Interfaces.Repositories;

public interface IImagesRepository
{
    Task Add(Image image);
    Task Delete(Guid id);
}