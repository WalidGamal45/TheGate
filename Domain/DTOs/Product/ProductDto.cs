﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "please enter the name")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "the name en must be english language")]
        public string NameE { get; set; }
        [Required(ErrorMessage = "الاسم العربى ")]
        [RegularExpression(@"^[\u0600-\u06FF\s]+$", ErrorMessage = "الاسم العربي يجب أن يكون باللغة العربية ")]
        public string NameA { get; set; }
        public int Price { get; set; }
        [Required(ErrorMessage ="enter the image")]
        public IFormFile? Imagefile { get; set; }
        [Range(1, int.MaxValue ,ErrorMessage ="please select category")]
        public int categoryId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "please select subcategory")]
        public int SubCategoryId { get; set; }

    }
}
