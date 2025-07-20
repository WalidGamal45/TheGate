using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Category
{
    public class AddCategryDto
    {
        [Required(ErrorMessage = "please enter the name")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "the name en must be english language")]
        public string NameE { get; set; }
        [Required(ErrorMessage = "بالرجاء ادخال الاسم العربى  ")]
        [RegularExpression(@"^[\u0600-\u06FF\s]+$", ErrorMessage = "الاسم العربي يجب أن يكون باللغة العربية ")]

        public string NameA { get; set; }

        public IFormFile Image { get; set; }

        public bool IsActive { get; set; }
    }

}
