using Clinic.Core.Models;

namespace Clinic.DataAccess.Repositories
{
    public interface IImagesRepository
    {
        Task Add(Image image);
        Task Delete(Guid id);
    }
}