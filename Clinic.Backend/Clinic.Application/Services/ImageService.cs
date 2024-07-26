using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Models;
using Clinic.DataAccess.Repositories;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Clinic.Application.Services;

public class ImageService : IImageService
{
    private readonly IImagesRepository _imagesRepository;

    public ImageService(IImagesRepository imagesRepository)
    {
        _imagesRepository = imagesRepository;
    }

    public async Task<Result<Image>> AddImage(IFormFile titleImage, string path)
    {
        try
        {
            var fileName = Path.GetFileName(titleImage.FileName);
            var filePath = Path.Combine(path, fileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await titleImage.CopyToAsync(stream);
            }


            var imageResult = Image.Create(Guid.NewGuid(), fileName, filePath);

            return imageResult;
        }
        catch (Exception ex)
        {
            return Result.Failure<Image>(ex.Message);
        }
    }

}
