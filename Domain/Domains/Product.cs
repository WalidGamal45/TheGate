﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domains
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "please enter the name")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "the name en must be english language")]
        public string NameE { get; set; }

        [Required(ErrorMessage = "الاسم العربى ")]
        [RegularExpression(@"^[\u0600-\u06FF\s]+$", ErrorMessage = "الاسم العربي يجب أن يكون باللغة العربية ")]
        public string NameA { get; set; }
        public int Price { get; set; }
        [Required]
        public string Imagep { get; set; }
        [ForeignKey("category")]
        public int categoryId { get; set; }
        public Category? category { get; set; }
        [ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }
        public SubCategory? SubCategory{ get; set; }
    }
}
