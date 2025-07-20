using Domain.Domains;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Category
{
    public int Id { get; set; }
    [Required(ErrorMessage = "please enter the name")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "the name en must be english language")]
    public string NameE { get; set; }

    [Required(ErrorMessage = "بالرجاء ادخال الاسم العربى  ")]
    [RegularExpression(@"^[\u0600-\u06FF\s]+$", ErrorMessage = "الاسم العربي يجب أن يكون باللغة العربية ")]
    public string NameA { get; set; }
  
    public string? Image { get; set; }

    public bool IsActive { get; set; }
    public List<SubCategory> SubCategories { get; set; }=new List<SubCategory>();
    public List<Product> Products { get; set; }=new List<Product>();
}
