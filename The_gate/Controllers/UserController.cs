using Application.Interfaces;
using Domain.Domains;
using Domain.DTOs.Users;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using The_gate.Extensions;

namespace The_gate.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser _user;
        private readonly ICategory _category;
        private readonly ISubCategory _subcategory;
        private readonly IProduct _product;

        public UserController(IUser user, ICategory category, ISubCategory subCategory, IProduct product)
        {
            _user = user;
            _category = category;
            _subcategory = subCategory;
            _product = product;
        }

        // ==================== User Auth ====================
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User user1)
        {
            var user2 = _user.GetUsers().FirstOrDefault(x => x.UserName == user1.UserName && x.PassWord == user1.PassWord);
            if (user2 == null)
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }
            if (!user2.IsConfirmed)
            {
                ViewBag.Error = "Your account is not confirmed yet. Please check your email.";
                return View();
            }
            return RedirectToAction("HomePageOfUser");
        }

        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                _user.Add(userDto);
                _user.Save();
                var savedUser = _user.GetByEmail(userDto.Email);
                SendConfirmationEmail(savedUser.Email, savedUser.VerificationCode);
                return RedirectToAction("ConfirmEmail", new { email = userDto.Email });
            }
            return View(userDto);
        }

        [HttpGet]
        public IActionResult ConfirmEmail(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        public IActionResult ConfirmEmail(string email, string verificationCode)
        {
            var userObj = _user.GetByEmail(email);
            if (userObj != null && userObj.VerificationCode == verificationCode)
            {
                userObj.IsConfirmed = true;
                _user.Edit(userObj, userObj.Id);
                _user.Save();
                return RedirectToAction("GetAllUsers");
            }
            ViewBag.Error = "Invalid code";
            return View();
        }

        private void SendConfirmationEmail(string toEmail, string code)
        {
            if (string.IsNullOrEmpty(toEmail)) return;
            try
            {
                var mail = new MailMessage
                {
                    From = new MailAddress("alhotelkbeer123@gmail.com"),
                    Subject = "Confirm your email",
                    Body = $"Your confirmation code is: {code}"
                };
                mail.To.Add(toEmail);
                using var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("alhotelkbeer123@gmail.com", "jrumxugnnghyxkdm"),
                    EnableSsl = true
                };
                smtp.Send(mail);
            }
            catch (Exception ex) { Console.WriteLine("Email Error: " + ex.Message); }
        }

        // ==================== User Management ====================
        public IActionResult GetAllUsers() => View(_user.GetUsers());

        public IActionResult Delete(int id)
        {
            var userObj = _user.GetById(id);
            if (userObj != null)
            {
                _user.Delete(id);
                _user.Save();
            }
            return RedirectToAction("GetAllUsers");
        }

        [HttpGet]
        public IActionResult Edit(int id) => View(_user.GetById(id));

        [HttpPost]
        public IActionResult Edit(User use, int id)
        {
            var userObj = _user.GetById(id);
            if (userObj != null)
            {
                _user.Edit(use, id);
                _user.Save();
            }
            return RedirectToAction("GetAllUsers");
        }

        // ==================== Categories & Products ====================
        public IActionResult HomePageOfUser() => View(_category.GetAll());

        [HttpGet]
        public IActionResult DisplaySubCategory(int id)
        {
            var list = _subcategory.GetAll().Where(s => s.categoryId == id).ToList();
            if (!list.Any()) { ViewBag.Message = "Not found SubCategories"; return View(new List<SubCategory>()); }
            return View(list);
        }

        [HttpGet]
        public IActionResult DisplayProduct(int id)
        {
            var list = _product.GetAll().Where(p => p.SubCategoryId == id).ToList();
            if (!list.Any()) { ViewBag.Message = "Not found Products"; return View(new List<Product>()); }
            return View(list);
        }

        // ==================== Cart / Basket ====================
        [HttpPost]
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var product = _product.GetById(id);
            if (product == null) return NotFound();

            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();
            var existing = cart.FirstOrDefault(x => x.ProductId == id);
            if (existing != null) existing.Quantity += quantity;
            else cart.Add(new CartItem { ProductId = product.Id, Name = product.NameE, Price = product.Price, Quantity = quantity });

            HttpContext.Session.SetObject("Cart", cart);
            HttpContext.Session.SetInt32("CartCount", cart.Sum(x => x.Quantity));

            return Json(new { cartCount = cart.Sum(x => x.Quantity) });
        }

        public IActionResult Invoice()
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();
            ViewBag.Total = cart.Sum(x => x.Price * x.Quantity).ToString("0.00");
            return View(cart);
        }

        [HttpPost]
        public IActionResult ChangeQuantity(int productId, int delta)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(x => x.ProductId == productId);

            if (item != null)
            {
                item.Quantity += delta;
                if (item.Quantity < 1) cart.Remove(item);
            }

            HttpContext.Session.SetObject("Cart", cart);
            HttpContext.Session.SetInt32("CartCount", cart.Sum(x => x.Quantity));

            decimal total = item != null ? item.Price * item.Quantity : 0;
            decimal grandTotal = cart.Sum(x => x.Price * x.Quantity);

            return Json(new
            {
                quantity = item?.Quantity ?? 0,
                total = total.ToString("0.00"),
                grandTotal = grandTotal.ToString("0.00"),
                cartCount = cart.Sum(x => x.Quantity)
            });
        }
    }
}
