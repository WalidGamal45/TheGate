using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domains
{
    public class SubCategory
    {
        public int Id { get; set; }

        public string NameE { get; set; }

        [Required(ErrorMessage = "الاسم العربى ")]
        [RegularExpression(@"^[\u0600-\u06FF\s]+$", ErrorMessage = "الاسم العربي يجب أن يكون باللغة العربية ")]
        public string NameA { get; set; }

        [Required]
        [NotMapped]
        public string Image { get; set; }

        public bool IsActive { get; set; }
        [ForeignKey("category")]
        public int categoryId { get; set; }
        public Category? category { get; set; }


    }
}
