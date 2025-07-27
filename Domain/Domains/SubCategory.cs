using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domains
{
    public class SubCategory
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "please enter the name")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "the name en must be english language")]
        public string NameE { get; set; }

        [Required(ErrorMessage = "الاسم العربى ")]
        [RegularExpression(@"^[\u0600-\u06FF\s]+$", ErrorMessage = "الاسم العربي يجب أن يكون باللغة العربية ")]
        public string NameA { get; set; }

        [Required]
        public string? Image { get; set; }

        public bool IsActive { get; set; }
        
        public int categoryId { get; set; }
        public Category? category { get; set; }


    }
}
