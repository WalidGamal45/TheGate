using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace The_gate.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly ISubCategory subCategory;
        public SubCategoryController(ISubCategory sub)
        {
            subCategory = sub;
        }
        public IActionResult Index()
        {
            var sub=subCategory.GetAdmins();
            return PartialView("_IndexToSubCategory",sub);
        }
    }
}
