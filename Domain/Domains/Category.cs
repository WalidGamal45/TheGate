using Domain.Domains;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Category
{
    public int Id { get; set; }

    public string NameE { get; set; }

    [Required(ErrorMessage = "الاسم العربى ")]
    [RegularExpression(@"^[\u0600-\u06FF\s]+$", ErrorMessage = "الاسم العربي يجب أن يكون باللغة العربية ")]
    public string NameA { get; set; }
  
    public string? Image { get; set; }

    public bool IsActive { get; set; }
    public List<SubCategory> SubCategories { get; set; }=new List<SubCategory>();
    public List<Product> Products { get; set; }=new List<Product>();
}
