using Microsoft.AspNetCore.Http;

namespace Domain.DTOs.Category
{
    public class AddCategryDto
    {
        public string NameE { get; set; }

        public string NameA { get; set; }

        public IFormFile Image { get; set; }

        public bool IsActive { get; set; }
    }

}
