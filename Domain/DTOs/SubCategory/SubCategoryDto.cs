using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.SubCategory
{
    public class SubCategoryDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "please enter the name")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "the name en must be english language")]
        public string NameE { get; set; }

        [Required(ErrorMessage = "الاسم العربى ")]
        [RegularExpression(@"^[\u0600-\u06FF\s]+$", ErrorMessage = "الاسم العربي يجب أن يكون باللغة العربية ")]
        public string NameA { get; set; }

        
        public IFormFile? Imagefile { get; set; }

        public bool IsActive { get; set; }
      
        public int categoryId { get; set; }
    }
}
