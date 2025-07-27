using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IImageService
    {
        string SaveImage(IFormFile file, string folderName);
    }
}
