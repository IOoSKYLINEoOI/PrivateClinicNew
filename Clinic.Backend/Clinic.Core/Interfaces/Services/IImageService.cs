using Clinic.Core.Models;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Services
{
    public interface IImageService
    {
        Task<Result<Image>> AddImage(IFormFile titleImage, string path);
    }
}