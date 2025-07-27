using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class ImageServices : IImageService
    {
        public string SaveImage(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0) return "";

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{folderName}");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"/{folderName}/" + uniqueFileName;
        }
    }
}
