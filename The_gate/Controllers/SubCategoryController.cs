using Application.Interfaces;
using Domain.Domains;
using Domain.DTOs.SubCategory;
using Microsoft.AspNetCore.Mvc;

namespace The_gate.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly ISubCategory subCategory;
        private readonly ICategory Category;
        private readonly IImageService _image;
        public SubCategoryController(ISubCategory sub, ICategory category, IImageService image)
        {
            subCategory = sub;
            Category = category;
            _image = image;
        }
        //****************************************************************************************
        public IActionResult Index()
        {
            var sub = subCategory.GetAll();
            return PartialView("_IndexToSubCategory", sub);
        }
        //****************************************************************************************
        [HttpGet]
        public IActionResult Add_SubCategory()
        {

            ViewBag.sub = Category.GetAll();
            return View();
        }
        //****************************************************************************************
        [HttpPost]
        public IActionResult Add_SubCategory(SubCategoryDto subcategory)
        {
            if (ModelState.IsValid)
            {

                string imagePath = _image.SaveImage(subcategory.Imagefile, "images");

                var cat = new SubCategory
                {
                    Image = imagePath,
                    IsActive = subcategory.IsActive,
                    NameA = subcategory.NameA,
                    NameE = subcategory.NameE,
                    categoryId = subcategory.categoryId
                };

                subCategory.Add(cat);
                subCategory.Save();

                return RedirectToAction("HomePage", "Admin");

            }

            ViewBag.sub = Category.GetAll();
            return View(subcategory);
        }

        //****************************************************************************************
        [HttpGet]
        public IActionResult Edit_SubCategory(int id)
        {
            ViewBag.sub = Category.GetAll();
            var cat = subCategory.GetById(id);
            var dto = new SubCategoryDto
            {
                NameA = cat.NameA,
                NameE = cat.NameE,
                IsActive = cat.IsActive,
                categoryId = cat.categoryId,
            };
            return View(dto);

        }
        //****************************************************************************************
        [HttpPost]
        public IActionResult Edit_SubCategory(SubCategoryDto category, int id)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = subCategory.GetById(id);
                if (existingCategory == null)
                {
                    return NotFound();
                }

                // إذا تم رفع صورة جديدة
                if (category.Imagefile != null && category.Imagefile.Length > 0)
                {
                    // حذف الصورة القديمة إن وجدت
                    if (!string.IsNullOrEmpty(existingCategory.Image))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingCategory.Image.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // حفظ الصورة الجديدة عن طريق الخدمة
                    string imagePath = _image.SaveImage(category.Imagefile, "images");
                    existingCategory.Image = imagePath;
                }

                // تحديث باقي البيانات
                existingCategory.NameA = category.NameA;
                existingCategory.NameE = category.NameE;
                existingCategory.IsActive = category.IsActive;
                existingCategory.categoryId = category.categoryId;

                subCategory.Update(existingCategory);
                subCategory.Save();

                return RedirectToAction("HomePage", "Admin");
            }

            ViewBag.sub = Category.GetAll();
            return View(category);

        }
        public IActionResult Delete(int id)
        {
            subCategory.Delete(id);
            subCategory.Save();
            return RedirectToAction("HomePage", "Admin");
        }
       
       


    }
}
