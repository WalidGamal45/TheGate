using Application.Interfaces;
using Domain.Domains;
using Domain.DTOs.Users;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;


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
            if (ModelState.IsValid)
            {
                user.Add(user3);
                user.Save();
                var savedUser = user.GetByEmail(user3.Email);

                SendConfirmationEmail(savedUser.Email, savedUser.VerificationCode);

                return RedirectToAction("ConfirmEmail", new { email = user3.Email });
            }

            return View(user3);
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
            var user3 = user.GetByEmail(email);
            if (user3 != null && user3.VerificationCode == verificationCode)
            {
                user3.IsConfirmed = true;
                user.Edit(user3, user3.Id);
                user.Save();
                return RedirectToAction("GetAllUsers");
            }

            ViewBag.Error = "Invalid code";
            return View();
        }
        private void SendConfirmationEmail(string toEmail, string code)
        {
            if (string.IsNullOrEmpty(toEmail))
                return;

            try
            {
                var mail = new MailMessage();
                mail.From = new MailAddress("yourEmail@gmail.com");
                mail.To.Add(toEmail);
                mail.Subject = "Confirm your email";
                mail.Body = $"Your confirmation code is: {code}";

                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("yourEmail@gmail.com", "yourAppPassword");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email Error: " + ex.Message);
            }
        

    }




}
}
