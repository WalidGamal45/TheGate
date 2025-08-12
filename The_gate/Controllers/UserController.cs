using Application.Interfaces;
using Domain.Domains;
using Microsoft.AspNetCore.Mvc;

namespace The_gate.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser user;
        private readonly ICategory _category;
        private readonly ISubCategory _subcategory;
        private readonly IProduct _product;

        public UserController(IUser _user, ICategory category, ISubCategory subCategory,IProduct product)
        {
            user = _user;
            _category = category;
            _subcategory = subCategory;
            _product = product;
        }
       
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user1)
        {
            var user2= user.GetUsers().FirstOrDefault(x=>x.UserName==user1.UserName&&x.PassWord==user1.PassWord);
            if (user2 != null)
            {
                return RedirectToAction("HomePageOfUser");
            }
            return View();
        }
        public IActionResult GetAllUsers()
        {
            var users=user.GetUsers();

            return View(users);
        }
       
        public IActionResult HomePageOfUser()
        {
            var catList = _category.GetAll();
            return View(catList);
        }

        [HttpGet]
        public IActionResult DisplaySubCategory(int id) // id = CategoryId
        {
            // أفضل لو عندك method مثل _subcategory.GetByCategoryId(id)
            var list = _subcategory.GetAll().Where(s => s.categoryId == id).ToList();
            return PartialView("_SubCategoryList", list);
        }

        [HttpGet]
        public IActionResult DisplayProduct(int id) // id = SubCategoryId
        {
            var list = _product.GetAll().Where(p => p.SubCategoryId == id).ToList();
            return PartialView("_ProductList", list);
        }
    }
}
