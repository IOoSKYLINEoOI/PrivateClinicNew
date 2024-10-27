using Clinic.Application.Services;
using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Models;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;

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
            // Получаем имя файла
            var fileName = Path.GetFileName(titleImage.FileName);
            var filePath = Path.Combine(path, fileName);

            // Проверка на существование файла
            if (File.Exists(filePath))
            {
                return Result.Failure<Image>("Файл с таким именем уже существует.");
            }

            // Сохранение файла на диск
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await titleImage.CopyToAsync(stream);
            }

            // Создание модели изображения
            var imageResult = Image.Create(Guid.NewGuid(), fileName, filePath);
            if (imageResult.IsFailure)
            {
                return Result.Failure<Image>(imageResult.Error);
            }

            // Сохранение изображения в базу данных
            await _imagesRepository.Add(imageResult.Value);

            return Result.Success(imageResult.Value);
        }
        catch (Exception ex)
        {
            return Result.Failure<Image>(ex.Message);
        }
    }

    public async Task<Image> GetImageById(Guid imageId)
    {
        var result = await _imagesRepository.GetById(imageId);
        return result; 
    }

}

