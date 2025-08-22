using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domains
{
    public class SubCategory
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "please enter the name")]
        [RegularExpression(@"^[A-Za-z0-9\s]+$", ErrorMessage = "The name must contain English letters and numbers only")]
        public string NameE { get; set; }

        [Required(ErrorMessage = "الاسم العربى ")]
        [RegularExpression(@"^[\u0600-\u06FF0-9\s]+$", ErrorMessage = "الاسم العربي يجب أن يحتوي على حروف عربية وأرقام فقط")]
        public string NameA { get; set; }

        [Required]
        public string? Image { get; set; }

        public bool IsActive { get; set; }
        
        public int categoryId { get; set; }
        public Category? category { get; set; }


    }
}
