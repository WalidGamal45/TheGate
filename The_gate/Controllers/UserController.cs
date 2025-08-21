using Application.Interfaces;
using Domain.Domains;
using Domain.DTOs.Users;
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
        [ValidateAntiForgeryToken]
        public IActionResult Login(User user1)
        {
            var user2= user.GetUsers().FirstOrDefault(x=>x.UserName==user1.UserName&&x.PassWord==user1.PassWord);
            if (user2 != null)
            {
                return RedirectToAction("HomePageOfUser");
            }
            ViewBag.Error = "the username or password is error";
            return View();
        }
        public IActionResult GetAllUsers()
        {
            var users=user.GetUsers();

            return View(users);
        }
        public IActionResult Delete(int id )
        {
            var use=user.GetById(id);
            if (use != null)
            {
                user.Delete(id);
                user.Save();
            }
            return RedirectToAction("GetAllUsers");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var use=user.GetById(id);
            return View(use);
        }
        [HttpPost]
        public IActionResult Edit(User use,int id)
        {
            var us=user.GetById(id);
            if (us != null)
            {
                user.Edit(use,id);
                user.Save();
            }
            return RedirectToAction("GetAllUsers");
        }


        public IActionResult HomePageOfUser()
        {
            var catList = _category.GetAll();
            return View(catList);
        }

        [HttpGet]
        public IActionResult DisplaySubCategory(int id)
        {
            var list = _subcategory.GetAll()
                .Where(s => s.categoryId == id)
                .ToList();

            if (!list.Any())  
            {
                ViewBag.Message = "Not found SubCategories ";
                return View(new List<SubCategory>());
            }

            return View(list);
        }


        [HttpGet]
        public IActionResult DisplayProduct(int id)
        {
            var list = _product.GetAll()
                .Where(p => p.SubCategoryId == id)
                .ToList();

            if (!list.Any()) 
            {
                ViewBag.Message = "Not found Products ";
                return View(new List<Product>()); 
            }

            return View(list);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserDto user3)
        {
            if (user3 != null)
            {
                user.Add(user3);
                user.Save();
            }
            return View();
        }
    }
}
